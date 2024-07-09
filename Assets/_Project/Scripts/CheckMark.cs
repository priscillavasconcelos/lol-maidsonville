using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMark : MonoBehaviour
{
    public GameObject CheckMarkPrefab;

    public void PlayerMadeIt()
    {
        CheckMarkPrefab.SetActive(true);
    }
}
