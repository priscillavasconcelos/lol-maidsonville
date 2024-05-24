using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointDiscoverInteraction : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _canvas;
    private BuildingSO _building;

    public event Action<BuildingSO> OnBuildingClicked; 

    public void OnPointerClick(PointerEventData eventData)
    {
        _canvas.SetActive(!_canvas.activeSelf);
        OnBuildingClicked?.Invoke(_building);
    }

    public void SetBuilding(BuildingSO building)
    {
        _building = building;
    }
}
