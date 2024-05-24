using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RampUpBuildingSO", menuName = "ScriptableObjects/RampUpBuildingSO")]
public class RampUpBuildingSO : ActionSO
{
    [SerializeField] private BuildingSO _building;
    [SerializeField] private float _secondsToFinish;
    [SerializeField] private int _cost;

    public float SecondsToFinish => _secondsToFinish;
    public int Cost => _cost;

    public override event Action<ActionSO> OnActionCompleted;

    public override void Initialize()
    {
        var buildings = FindObjectsOfType<BuildingInteraction>();
        foreach (var building in buildings)
        {
            if (building.Building != _building)
                continue;

            building.OnBuildingClicked += (building) => PerformAction();
            break;
        }
    }

    public override void PerformAction()
    {
        OnActionCompleted?.Invoke(this);
    }
}
