using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScienceManager : MonoBehaviour
{
    public static ScienceManager Instance;

    [SerializeField] bool tsunamiReady; 
    [SerializeField] bool earthquakeReady; 
    [SerializeField] bool hurricaneReady; 
    [SerializeField] bool forestFireReady; 

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public void TsunamiUpgradeBtn()
    {
        tsunamiReady = true;
    }

    public void EarthquakeUpgradeBtn()
    {
        earthquakeReady = true;
    }

    public void HurricaneUpgradeBtn()
    {
        hurricaneReady = true;
    }

    public void ForestFireUpgradeBtn()
    {
        forestFireReady = true;
    }

    public void TsunamiTime(DialogueSO upgraded, DialogueSO withoutUpgrade)
    {
        if(tsunamiReady)
        {
            DialogueView.Instance.SetDialogue(upgraded.Dialogue);

            return;
        }

        DialogueView.Instance.SetDialogue(withoutUpgrade.Dialogue);
    }

    public void EarthquakeTime(DialogueSO upgraded, DialogueSO withoutUpgrade)
    {
        if (earthquakeReady)
        {
            DialogueView.Instance.SetDialogue(upgraded.Dialogue);

            return;
        }

        DialogueView.Instance.SetDialogue(withoutUpgrade.Dialogue);
    }

    public void HurricaneTime(DialogueSO upgraded, DialogueSO withoutUpgrade)
    {
        if (hurricaneReady)
        {
            DialogueView.Instance.SetDialogue(upgraded.Dialogue);

            return;
        }

        DialogueView.Instance.SetDialogue(withoutUpgrade.Dialogue);
    }

    public void ForestFireTime(DialogueSO upgraded, DialogueSO withoutUpgrade)
    {
        if (forestFireReady)
        {
            DialogueView.Instance.SetDialogue(upgraded.Dialogue);

            return;
        }

        DialogueView.Instance.SetDialogue(withoutUpgrade.Dialogue);
    }
}
