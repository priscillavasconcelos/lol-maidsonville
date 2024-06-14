using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingSO", menuName = "ScriptableObjects/BuildingSO")]
public class BuildingSO : ScriptableObject
{
    [SerializeField] private string _buildingName;
    [SerializeField] private List<RampUpBuildingSO> _buildingUpgrades = new List<RampUpBuildingSO>();

    public string BuildingName => _buildingName;
}
