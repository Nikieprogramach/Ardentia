using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIconQuest : MonoBehaviour
{
    public QuestGiver QuestGiver;
    public GameObject itemInfoObj;

    public void OnMouseOver()
    {
        itemInfoObj.GetComponent<ItemInfo>().ShowInfo(QuestGiver.quest.itemReward);
    }

    public void OnMouseExit()
    {
        itemInfoObj.GetComponent<ItemInfo>().HideInfo();
    }
}
