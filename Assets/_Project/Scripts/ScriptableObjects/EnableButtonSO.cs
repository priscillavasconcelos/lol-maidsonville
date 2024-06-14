using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnableButtonActionSO", menuName = "ScriptableObjects/EnableButtonActionSO")]

public class EnableButtonSO : ActionSO
{
    [SerializeField] private BuildingSO _building;

    public override event Action<ActionSO> OnActionCompleted;

    public override void Initialize()
    {
        var buildings = FindObjectsOfType<BuildingInteraction>();
        foreach (var building in buildings)
        {
            if (building.Building != _building)
                continue;

            OnActionCompleted?.Invoke(this);
            building._button.enabled = true;
            break;
        }
    }

    public override void PerformAction()
    {

    }
}
