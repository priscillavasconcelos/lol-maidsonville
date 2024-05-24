using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueSO))]
public class DialogueSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (DialogueSO)target;

        GUILayout.Space(20);
        if(GUILayout.Button("Turn to Json"))
        {
            script.TurnToJson();
        }
        
    }
}
