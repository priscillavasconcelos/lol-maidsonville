using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resultados : MonoBehaviour
{
    [SerializeField] List<GameObject> gameObjects = new List<GameObject>();
    [SerializeField] GameObject arrowDown;
    [SerializeField] GameObject arrowUp;
    private bool showResults = false;

    public void ShowAndHideActions()
    {
        if (!showResults)
        {
            arrowDown.SetActive(false);
            arrowUp.SetActive(true);
            
            foreach (GameObject obj in gameObjects) 
            {
                obj.SetActive(true);
            }

            showResults = true;
            return;
        }
        if (showResults)
        {
            arrowDown.SetActive(true);
            arrowUp.SetActive(false);

            foreach (GameObject obj in gameObjects)
            {
                obj.SetActive(false);
            }

            showResults = false;
        }
    }
}
