using UnityEngine;

[CreateAssetMenu(fileName = "BuildingSO", menuName = "ScriptableObjects/BuildingSO")]
public class BuildingSO : ScriptableObject
{
    [SerializeField] private string _buildingName;

    public string BuildingName => _buildingName;
}
