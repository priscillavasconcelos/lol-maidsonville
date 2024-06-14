using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBuildingMessage : MonoBehaviour
{
    [SerializeField] DialogueSO buildingDialogue;

    public void DoUpgrade(GameObject _building)
    {
        if(DialogueView.Instance._dialogueWindow.activeInHierarchy)
            return;

        DialogueView.Instance.SetDialogue(buildingDialogue.Dialogue);
        DialogueView.Instance.choices.SetActive(true);
        DialogueView.Instance.buildingSelected = _building;
        DialogueView.Instance.ChangeBuildingSelected();
    }

    public void ChangeText(string text)
    {
        DialogueView.Instance.yesText.text = text;
    }
}
