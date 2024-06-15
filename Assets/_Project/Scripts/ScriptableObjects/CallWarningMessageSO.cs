using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CallWarningMessageSO", menuName = "ScriptableObjects/CallWarningMessageSO")]
public class CallWarningMessageSO : ActionSO
{
    [SerializeField] DialogueSO warningWithoutUpgrade;
    [SerializeField] DialogueSO warningWithUpgrade;

    [SerializeField] bool tsunami;
    [SerializeField] bool earthquake;
    [SerializeField] bool hurricane;
    [SerializeField] bool forestFire;

    public override event Action<ActionSO> OnActionCompleted;

    public override void Initialize()
    {
        if(tsunami)
        {
            ScienceManager.Instance.TsunamiTime(warningWithUpgrade, warningWithoutUpgrade);
        }
        else if (earthquake)
        {
            ScienceManager.Instance.EarthquakeTime(warningWithUpgrade, warningWithoutUpgrade);
        }
        else if (hurricane) 
        {
            ScienceManager.Instance.HurricaneTime(warningWithUpgrade, warningWithoutUpgrade);
        }
        else if(forestFire)
        {
            ScienceManager.Instance.ForestFireTime(warningWithUpgrade, warningWithoutUpgrade);
        }
        else
        {
            Debug.LogWarning("Não foi selecionado uma booleana do tipo de desastre que está avisando");
        }
        
        OnActionCompleted?.Invoke(this);

    }

    public override void PerformAction()
    {

    }
}
