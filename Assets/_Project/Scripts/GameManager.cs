using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [System.Serializable]
    public class GroupsOfBuildings
    {
        public string disasterName;
        public List<GameObject> buildings = new List<GameObject>();
    }

    [SerializeField] List<GroupsOfBuildings> disasterToCheckBuildings = new List<GroupsOfBuildings>();
    [SerializeField] int currentGroupChecked;

    [SerializeField] DialogueSO gameOverDialogue;
    [SerializeField] DialogueSO positiveFeedbackDialogue;
    [SerializeField] DialogueSO somethingWrongFeedbackDialogue;

    [SerializeField] private int _moneyAmountReceived;

    public GameObject resultsScreen;

    public bool noMoney = false;
    private bool mistake = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        resultsScreen.SetActive(false);
    }

    public void MoneyReceivedAtEndOfWeek()
    {
        TopHudView.Instance.UpdateMoneyAmount(_moneyAmountReceived);
    }

    public void ReceiveMoneyForDisaster()
    {
        Debug.Log(1);

        // Ensure currentGroupChecked is within the valid range
        if (currentGroupChecked < 0 || currentGroupChecked >= disasterToCheckBuildings.Count)
        {
            Debug.LogWarning("currentGroupChecked is out of range");
            return;
        }

        // Get the current group of buildings
        GroupsOfBuildings currentGroup = disasterToCheckBuildings[currentGroupChecked];
        int trueCount = 0;

        // Iterate through the list of GameObjects to check their boolean status
        foreach (GameObject obj in currentGroup.buildings)
        {
            if (obj.TryGetComponent<BuildingInteraction>(out var component) && component.isUpgraded)
            {
                trueCount++;
            }
            if (obj.TryGetComponent<BuildingInteraction>(out var build) && !build.isUpgraded)
            {
                mistake = true;
                Debug.Log("mistake");
            }
        }
        
        // Calculate the fraction of true booleans
        int totalBuildings = currentGroup.buildings.Count;
        float fractionTrue = (float)trueCount / totalBuildings;

        // Calculate the amount to be received based on the fraction of true booleans
        int moneyAmountToReceive = Mathf.RoundToInt(_moneyAmountReceived * fractionTrue);

        // Update the money amount in the HUD
        TopHudView.Instance.UpdateMoneyAmount(moneyAmountToReceive);

        currentGroupChecked++;
    }

    public void FinishGame()
    {
        if (!mistake)
        {
            DialogueView.Instance.SetDialogue(positiveFeedbackDialogue.Dialogue);
        }
        else
        {
            DialogueView.Instance.SetDialogue(somethingWrongFeedbackDialogue.Dialogue);
        }

        resultsScreen.SetActive(true);
    }

    public void GameOver()
    {
        if (noMoney)
        {
            DialogueView.Instance.SetDialogue(gameOverDialogue.Dialogue);
            resultsScreen.SetActive(true);
        }
    }
}
