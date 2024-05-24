using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WaitForBuildingInteractionActionSO", menuName = "ScriptableObjects/WaitForBuildingInteractionActionSO")]
public class WaitForBuildingInteractionActionSO : ActionSO
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

            building.ToggleInteraction(true);
            building.OnBuildingClicked += (building) => PerformAction();
            break;
        }
    }

    public override void PerformAction()
    {
        OnActionCompleted?.Invoke(this);
    }
}
