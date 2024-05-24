using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionSO", menuName = "ScriptableObjects/ActionSO")]
public abstract class ActionSO : ScriptableObject
{
    public abstract event Action<ActionSO> OnActionCompleted; 
    public abstract void Initialize();
    public abstract void PerformAction();
}
