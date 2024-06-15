using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FinishTutorialDialogueSO", menuName = "ScriptableObjects/FinishTutorialDialogueSO")]
public class FinishTutorialDialogueSO : ActionSO
{
    public override event Action<ActionSO> OnActionCompleted;

    public override void Initialize()
    {
        var dialogueManager = FindObjectOfType<DialogueManager>();

        if(dialogueManager != null)
        {
            dialogueManager.EndTutorial();
        }

        OnActionCompleted?.Invoke(this);
    }

    public override void PerformAction()
    {
        
    }
}
