using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueSO _dialogue;

    private void Start()
    {
        DialogueView.Instance.SetDialogue(_dialogue.Dialogue);
    }
}
