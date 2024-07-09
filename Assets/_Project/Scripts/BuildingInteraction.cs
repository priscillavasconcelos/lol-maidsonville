using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingInteraction : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private BuildingSO _building;
    [SerializeField] private Collider2D _collider;
    public GameObject _discoverPoint;
    [SerializeField] private GameObject _canvas;
    
    [SerializeField] private GameObject _constructionImproved;
    [SerializeField] private GameObject _constructionDestroyed;

    [SerializeField] private CheckMark checkMark;
    
    public Button _button;
    public GameObject buttonHolder;
    [SerializeField] private Button constructionButton;
    [SerializeField] private RampUpBuildingSO _rampUpBuilding;
    public bool isUpgraded;
    
    [SerializeField] private Image _timerImage;

    public static event Action<RampUpBuildingSO> OnConstructionStarted;
    public static event Action<RampUpBuildingSO> OnConstructionFinished;

    public BuildingSO Building => _building;

    public event Action<BuildingSO> OnBuildingClicked; 
    
    private void Awake()
    {
        _button.onClick.AddListener(StartConstruction);
    }

    private void Start()
    {
        transform.name = _building.BuildingName;
        ToggleDiscoverPoint(false);
    }

    private void Update()
    {
        if (constructionButton == null)
            return;

        if (_discoverPoint.activeInHierarchy)
        {
            constructionButton.gameObject.SetActive(false);
        }
        else
        {
            constructionButton.gameObject.SetActive(true);
            
            if(_timerImage.gameObject.activeInHierarchy)
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
    
    public void CheckForRepairs(DisasterSO disaster)
    {
        if (isUpgraded)
        {
            TopHudView.Instance.UpdateMoneyAmount(disaster.CostToRepairUpgraded);
        }
        else
        {
            TopHudView.Instance.UpdateMoneyAmount(disaster.CostToRepairWithoutUpgraded);
            _constructionDestroyed.SetActive(true) ;

            GameManager.Instance.GameOver();
        }
    }

    public void StartConstruction()
    {
        StartCoroutine(Construct());
    }

    public void ToggleConstruction(bool active)
    {
        _button.gameObject.SetActive(active);
    }

    private IEnumerator Construct()
    {
        TopHudView.Instance.UpdateMoneyAmount(_rampUpBuilding.Cost);
        float duration = _rampUpBuilding.SecondsToFinish; 
        
        OnConstructionStarted?.Invoke(_rampUpBuilding);
        _timerImage.gameObject.SetActive(true);
        buttonHolder.gameObject.SetActive(false);
        isUpgraded = true;
        
        float normalizedTime = 0;
        while(normalizedTime <= 1f)
        {
            _timerImage.fillAmount = normalizedTime;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        
        _rampUpBuilding.PerformAction();
        
        if(_constructionImproved != null)
            _constructionImproved.SetActive(true);

        if(checkMark != null)
            checkMark.PlayerMadeIt();

        OnConstructionFinished?.Invoke(_rampUpBuilding);

        ToggleDiscoverPoint(false);
    }
}
