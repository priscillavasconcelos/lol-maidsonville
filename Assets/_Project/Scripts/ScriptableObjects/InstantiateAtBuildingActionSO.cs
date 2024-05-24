using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InstantiateAtBuildingActionSO", menuName = "ScriptableObjects/InstantiateAtBuildingActionSO")]
public class InstantiateAtBuildingActionSO : ActionSO
{
    [SerializeField] private PointDiscoverInteraction _object;
    [SerializeField] private BuildingSO _building;

    public override event Action<ActionSO> OnActionCompleted;

    public override void Initialize()
    {
        var buildings = FindObjectsOfType<BuildingInteraction>();
        foreach (var building in buildings)
        {
            if (building.Building != _building)
                continue;
            
            var point = Instantiate(_object.gameObject).GetComponent<PointDiscoverInteraction>();
            point.transform.SetParent(building.transform);
            point.transform.localPosition = Vector3.zero;
            point.SetBuilding(building.Building);
            break;
        }
        OnActionCompleted?.Invoke(this);
    }

    public override void PerformAction()
    {
        
    }
}
