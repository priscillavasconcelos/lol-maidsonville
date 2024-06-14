using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DisasterSO", menuName = "ScriptableObjects/DisasterSO")]
public class DisasterSO : ActionSO
{
    [SerializeField] private string _disasterName;

    [SerializeField] private List<BuildingSO> _buildings = new List<BuildingSO>();
    [SerializeField] private int _costToRepairUpgraded;
    [SerializeField] private int _costToRepairWithoutUpgrade;

    public string DisasterName => _disasterName;

    public int CostToRepairUpgraded => _costToRepairUpgraded;
    public int CostToRepairWithoutUpgraded => _costToRepairWithoutUpgrade;

    public override event Action<ActionSO> OnActionCompleted;

    public override void Initialize()
    {
        var disasters = FindObjectsOfType<DisasterAppears>();
        foreach (var disaster in disasters)
        {
            if(disaster.Disaster._disasterName == _disasterName)
            {
                disaster.DisasterHappens();
            }
        }

        var buildings = FindObjectsOfType<BuildingInteraction>();
        foreach (var building in buildings)
        {
            foreach (var targetBuilding in _buildings) // Itera sobre a lista de _buildings
            {
                if (building.Building == targetBuilding)
                {
                    building.CheckForRepairs(this);
                    // Aciona o evento ao encontrar uma correspondência
                    OnActionCompleted?.Invoke(this);
                }
            }
        }
    }

    public override void PerformAction()
    {

    }
}
