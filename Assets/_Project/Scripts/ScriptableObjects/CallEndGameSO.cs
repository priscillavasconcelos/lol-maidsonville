using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CallEndGameSO", menuName = "ScriptableObjects/CallEndGameSO")]
public class CallEndGameSO : ActionSO
{
    public override event Action<ActionSO> OnActionCompleted;

    public override void Initialize()
    {
        GameManager.Instance.FinishGame();
        OnActionCompleted?.Invoke(this);
    }

    public override void PerformAction()
    {
       
    }
}
