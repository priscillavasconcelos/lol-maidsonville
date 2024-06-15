using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopHudView : HudView<TopHudView>
{
    [SerializeField] private TMP_Text _moneyAmountText;
    [SerializeField] private TMP_Text _timerText;
    
    [SerializeField] private Button _info;
    [SerializeField] private Button _menu;
    [SerializeField] private Button _settings;

    [SerializeField] private int _moneyAmount;

    public int MoneyAmount => _moneyAmount;

    private void OnEnable()
    {
        _info.onClick.AddListener(DisplayInfo);
        _menu.onClick.AddListener(DisplayMenu);
        _settings.onClick.AddListener(DisplaySettings);

        UpdateMoneyAmount(0);
    }

    private void OnDisable()
    {
        _info.onClick.RemoveListener(DisplayInfo);
        _menu.onClick.RemoveListener(DisplayMenu);
        _settings.onClick.RemoveListener(DisplaySettings);
    }

    public void UpdateMoneyAmount(int newValue)
    {
        _moneyAmount += newValue;
        _moneyAmountText.text = _moneyAmount.ToString();

        if (GameManager.Instance == null)
            return;

        if(_moneyAmount < 0)
        {
            GameManager.Instance.noMoney = true;
        }
        else
        {
            GameManager.Instance.noMoney = false;
        }
    }

    public void UpdateTimer(string newValue)
    {
        _timerText.text = newValue;
    }

    private void DisplayInfo()
    {
        
    }

    private void DisplayMenu()
    {
        
    }
    
    private void DisplaySettings()
    {
        
    }

}
