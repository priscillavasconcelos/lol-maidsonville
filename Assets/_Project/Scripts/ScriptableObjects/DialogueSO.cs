using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "ScriptableObjects/DialogueSO")]
public class DialogueSO : ScriptableObject
{
    public Dialogue Dialogue;
    
    [TextArea(10,100)]
    public string ToJson;

    public void TurnToJson()
    {
        ToJson = "";
        string temp = "";
        foreach (var phrase in Dialogue.Phrases)
        {
            temp = $"\"{phrase.Id}\" : \"{phrase.Text}\",\n";
            ToJson = String.Concat(ToJson, temp);
        }
    }
}
