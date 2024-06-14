using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisasterAppears : MonoBehaviour
{
    [SerializeField] private DisasterSO _disaster;
    [SerializeField] GameObject disasterImage;

    public DisasterSO Disaster => _disaster;

    public void DisasterHappens()
    {
        disasterImage.SetActive(true);
    }

    public void DisasterPassed()
    {
        disasterImage.SetActive(false);
    }
}
