using UnityEngine;

[System.Serializable]
public class Quest
{
    public enum QuestType { Gathering, Talk, Reach };
    public enum QuestStatus { Waitting, Accepted, Completed};

    public string questName;
    public QuestType questType;
    public QuestStatus questStatus;

    public int expReward;
    public int goldReward;

    [Header("Gathering Type Quest")]
    public int requireAmount;
}

//TODO 可以用【ScriptableObject】替换
