using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public GameObject questWindow;
    private bool questWindowIsShowed = false;

    public GameObject hasQuestMark;
    public GameObject hasCompletedQuestMark;
    public bool isGivingrewardForQuest = false;

    public bool isGivingQuest = true;

    public Quest quest;

    public Text title;
    public Text description;
    public GameObject money;
    public Text moneyAmount;
    public GameObject xp;
    public Text xpAmount;
    public GameObject item;
    public Image itemIcon;

    //Reward window
    public GameObject rewardWindow;

    public Text rewardTitle;
    public GameObject rewardMoney;
    public Text rewardMoneyAmount;
    public GameObject rewardXp;
    public Text rewardXpAmount;
    public GameObject rewardItem;
    public Image rewardItemIcon;

    public void OpenQuestWindow()
    {
        if (isGivingQuest == true)
        {
            if (questWindowIsShowed)
            {
                questWindow.SetActive(false);
                questWindowIsShowed = false;
            }
            else
            {
                questWindow.SetActive(true);
                questWindowIsShowed = true;

                title.text = quest.title;
                description.text = quest.description;
                if (quest.moneyReward != null)
                {
                    money.SetActive(true);
                    moneyAmount.text = quest.moneyReward.ToString();
                }
                if (quest.xpReward != null)
                {
                    xp.SetActive(true);
                    xpAmount.text = quest.xpReward.ToString();
                }
                if (quest.itemReward != null)
                {
                    item.gameObject.SetActive(true);
                    itemIcon.sprite = quest.itemReward.icon;
                }
            }
        }else if (isGivingrewardForQuest)
        {
            rewardWindow.SetActive(true);

            rewardTitle.text = quest.title + " completed!";
            if (quest.moneyReward != null)
            {
                rewardMoney.SetActive(true);
                rewardMoneyAmount.text = quest.moneyReward.ToString();
            }
            if (quest.xpReward != null)
            {
                rewardXp.SetActive(true);
                rewardXpAmount.text = quest.xpReward.ToString();
            }
            if (quest.itemReward != null)
            {
                rewardItem.gameObject.SetActive(true);
                rewardItemIcon.sprite = quest.itemReward.icon;
            }
        }
    }

    void Update()
    {
        if(QuestTracker.instance.activeQuest != null && QuestTracker.instance.questIsCompleted == true && QuestTracker.instance.activeQuest.title == quest.title)
        {
            hasCompletedQuestMark.SetActive(true);
            isGivingrewardForQuest = true;
        }
    }

    public void AcceptQuest()
    {
        Debug.Log("Quest given");
        QuestTracker.instance.activeQuest = quest;
        QuestTracker.instance.activeQuestsText.SetActive(true);
        QuestTracker.instance.questGameObject.SetActive(true);
        QuestTracker.instance.questName.text = quest.title;
        QuestTracker.instance.questProgres.text = 0 + "/" + quest.amountToCompleteQuest + " " + quest.tagOfRequirements;
        questWindow.SetActive(false); 
        questWindowIsShowed = false;
        isGivingQuest = false;
        hasQuestMark.SetActive(false);
    }
    
    public void OnCompleteQuestPress()
    {
        QuestTracker.instance.CompleteQuest();
        rewardWindow.SetActive(false);
    }
}
