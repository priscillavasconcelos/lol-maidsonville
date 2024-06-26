using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueSO _dialogue;
    [SerializeField] private DialogueSO _dialogueEndTutorial;

    private void Start()
    {
        DialogueView.Instance.SetDialogue(_dialogue.Dialogue);
    }

    public void CallEndTutorial()
    {
        StartCoroutine(EndTutorial());
    }

    IEnumerator EndTutorial()
    {
        if (MessageStackController.Instance.transform.childCount > 0)
        {
            bool allChildrenInactive = true;

            // Iterate through each child GameObject
            foreach (Transform child in MessageStackController.Instance.transform)
            {
                // Check if the child GameObject is active
                if (child.gameObject.activeSelf)
                {
                    allChildrenInactive = false;
                    break;
                }
            }


            yield return new WaitUntil(() => !DialogueView.Instance._dialogueWindow.activeInHierarchy);

            if (allChildrenInactive)
            {
                DialogueView.Instance.SetDialogue(_dialogueEndTutorial.Dialogue);
                FindObjectOfType<TimeManager>().timeRunning = true;
            }
            
        }
    }
}
