using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddMoneySO", menuName = "ScriptableObjects/AddMoneySO")]
public class AddMoneySO : ActionSO
{
    public override event Action<ActionSO> OnActionCompleted;

    public override void Initialize()
    {
        GameManager.Instance.MoneyReceivedAtEndOfWeek();
        OnActionCompleted?.Invoke(this);
    }

    public override void PerformAction()
    {
        
    }
}
