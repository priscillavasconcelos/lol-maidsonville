using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FinishGameSO", menuName = "ScriptableObjects/FinishGameSO")]
public class FinishGameSO : ActionSO
{
    public override event Action<ActionSO> OnActionCompleted;

    public override void Initialize()
    {
        var dialogueManager = FindObjectOfType<GameManager>();

        if (dialogueManager != null)
        {
            dialogueManager.ResultScreen();
        }

        OnActionCompleted?.Invoke(this);
    }

    public override void PerformAction()
    {

    }
}
