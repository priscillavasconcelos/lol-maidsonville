using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AddMessageToStackActionSO", menuName = "ScriptableObjects/AddMessageToStackActionSO")]
public class AddMessageToStackActionSO : ActionSO
{
    [SerializeField] private DialogueSO _message;

    public override event Action<ActionSO> OnActionCompleted;

    public override void Initialize()
    {
        MessageStackController.Instance.AddMessageToStack(_message.Dialogue);
        OnActionCompleted?.Invoke(this);
    }

    public override void PerformAction()
    {
        
    }
}
