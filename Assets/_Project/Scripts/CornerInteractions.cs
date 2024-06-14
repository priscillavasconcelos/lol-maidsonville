using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CornerInteractions : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private BuildingSO _building;
    [SerializeField] private Collider2D _collider;
    public GameObject _discoverPoint;
    [SerializeField] private GameObject _canvas;

    [SerializeField] private GameObject _constructionImprovedLight;
    [SerializeField] private GameObject _constructionImprovedHydrant;

    public GameObject buttonHolder;
    [SerializeField] Button _buttonLightpole;
    [SerializeField] Button _buttonHydrant;
    [SerializeField] GameObject[] constructionButtons;
    [SerializeField] private RampUpBuildingSO _rampUpBuildingLight;
    [SerializeField] private RampUpBuildingSO _rampUpBuildingHydrant;
    public bool lightConstruct = false;

    [SerializeField] private Image _timerImage;

    public static event Action<RampUpBuildingSO> OnConstructionStarted;
    public static event Action<RampUpBuildingSO> OnConstructionFinished;

    public BuildingSO Building => _building;

    public event Action<BuildingSO> OnBuildingClicked;

    private void Awake()
    {
        _buttonLightpole.onClick.AddListener(StartConstruction);
        _buttonLightpole.onClick.AddListener(Lightpole);
        _buttonHydrant.onClick.AddListener(StartConstruction);
        _buttonHydrant.onClick.AddListener(Hydrant);
    }

    private void Start()
    {
        transform.name = _building.BuildingName;
        ToggleDiscoverPoint(false);
    }

    private void Update()
    {
        if (_discoverPoint.activeInHierarchy)
        {
            for (int i = 0; i < constructionButtons.Length; i++)
            {
                constructionButtons[i].gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < constructionButtons.Length; i++)
            {
                constructionButtons[i].gameObject.SetActive(true);
            }

            if (_timerImage.gameObject.activeInHierarchy)
                _timerImage.gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _canvas.SetActive(true);
        buttonHolder.gameObject.SetActive(true);
        OnBuildingClicked?.Invoke(Building);
    }

    public void ToggleInteraction(bool active)
    {
        _collider.enabled = active;
    }

    public void ToggleDiscoverPoint(bool active)
    {
        _discoverPoint.SetActive(active);
    }

    public void BuildingLocation(GameObject place)
    {
        _discoverPoint.transform.position = place.transform.position;
        buttonHolder.transform.position = place.transform.position;
        _timerImage.transform.position = place.transform.position;
    }

    private void StartConstruction()
    {
        StartCoroutine(Construct());
    }

    public void ToggleConstruction(bool active)
    {
        buttonHolder.gameObject.SetActive(active);
    }

    private void Lightpole()
    {
        lightConstruct = true;
    }    
    
    private void Hydrant()
    {
        lightConstruct = false;
    }

    private IEnumerator Construct()
    {
        yield return new WaitForEndOfFrame();
        if(lightConstruct == true)
        {
            TopHudView.Instance.UpdateMoneyAmount(_rampUpBuildingLight.Cost);
            float duration = _rampUpBuildingLight.SecondsToFinish;

            OnConstructionStarted?.Invoke(_rampUpBuildingLight);
            _timerImage.gameObject.SetActive(true);
            buttonHolder.gameObject.SetActive(false);

            float normalizedTime = 0;
            while (normalizedTime <= 1f)
            {
                _timerImage.fillAmount = normalizedTime;
                normalizedTime += Time.deltaTime / duration;
                yield return null;
            }

            _rampUpBuildingLight.PerformAction();

            _constructionImprovedLight.SetActive(true);
            OnConstructionFinished?.Invoke(_rampUpBuildingLight);

            ToggleDiscoverPoint(false);
        }
        else
        {
            TopHudView.Instance.UpdateMoneyAmount(_rampUpBuildingHydrant.Cost);
            float duration = _rampUpBuildingHydrant.SecondsToFinish;

            OnConstructionStarted?.Invoke(_rampUpBuildingHydrant);
            _timerImage.gameObject.SetActive(true);
            buttonHolder.gameObject.SetActive(false);

            float normalizedTime = 0;
            while (normalizedTime <= 1f)
            {
                _timerImage.fillAmount = normalizedTime;
                normalizedTime += Time.deltaTime / duration;
                yield return null;
            }

            _rampUpBuildingLight.PerformAction();

            _constructionImprovedHydrant.SetActive(true);
            OnConstructionFinished?.Invoke(_rampUpBuildingHydrant);

            ToggleDiscoverPoint(false);
        }
    }
}
