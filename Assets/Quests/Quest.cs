using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/Quest")]
public class Quest : ScriptableObject
{
    public string title;
    public string description;

    public QuestType type;
    public string tagOfRequirements;
    public int amountToCompleteQuest;

    public int xpReward;
    public int moneyReward;
    public Item itemReward;
}

public enum QuestType { Kill, Gathering }
