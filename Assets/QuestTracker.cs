using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTracker : MonoBehaviour
{
    public static QuestTracker instance;

    void Awake()
    {
        instance = this;
    }

    public GameObject activeQuestsText;
    public GameObject questGameObject;
    public Text questName;
    public Text questProgres;

    public Quest activeQuest;

    public bool questIsCompleted = false;

    public int progresTowardQuest;

    public void OnEnemyKilled(GameObject enemyKilled) 
    {
        if(activeQuest != null && activeQuest.type.ToString() == "Kill")
        {
            if(enemyKilled.tag == activeQuest.tagOfRequirements)
            {
                progresTowardQuest ++;

                questProgres.text = progresTowardQuest + "/" + activeQuest.amountToCompleteQuest + " " + activeQuest.tagOfRequirements;

                if (progresTowardQuest >= activeQuest.amountToCompleteQuest)
                {
                    questProgres.text = "Completed";
                    questIsCompleted = true;
                }
            }
        }
    }

    public void CompleteQuest()
    {
        Debug.Log("Quest completed");
        if(activeQuest.moneyReward != 0)
        {
            PlayerController.instance.AddMoney(activeQuest.moneyReward);
        }
        if(activeQuest.itemReward != null)
        {
            Inventory.instance.AddItem(activeQuest.itemReward);
        }
        questIsCompleted = false;
        activeQuestsText.SetActive(false);
        questGameObject.SetActive(false);
        activeQuest = null;
    }
}
