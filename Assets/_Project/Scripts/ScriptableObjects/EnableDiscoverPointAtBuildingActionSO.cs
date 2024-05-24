using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnableDiscoverPointAtBuildingActionSO", menuName = "ScriptableObjects/EnableDiscoverPointAtBuildingActionSO")]
public class EnableDiscoverPointAtBuildingActionSO : ActionSO
{
    [SerializeField] private BuildingSO _building;
    
    [SerializeField] private bool _enabled;

    public override event Action<ActionSO> OnActionCompleted;

    public override void Initialize()
    {
        var buildings = FindObjectsOfType<BuildingInteraction>();
        foreach (var building in buildings)
        {
            if (building.Building != _building)
                continue;

            building.ToggleDiscoverPoint(_enabled);
            break;
        }
        OnActionCompleted?.Invoke(this);
    }

    public override void PerformAction()
    {
        
    }
}
