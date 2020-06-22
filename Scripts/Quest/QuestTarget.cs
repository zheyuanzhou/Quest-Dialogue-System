using UnityEngine;

//MARKER 这个脚本将会放在【所有和任务完成】有关的游戏对象上
//比如说可收集的物品、隐藏的NPC、探索的区域等
public class QuestTarget : MonoBehaviour
{
    public string questName;

    public enum QuestType { Gathering, Talk, Reach };
    public QuestType questType;

    [Header("Talk Type Quest")]
    public bool hasTalked;

    [Header("Reach Type Quest")]
    public bool hasReach;

    //MARKER 这个方法会在【完成的时候】触发
    //比如说，NPC对话完成后、到达探索区域、收集完物品
    public void CheckQuestIsComplete()
    {
        for(int i = 0; i < Player.instance.questList.Count; i++)
        {
            if (questName == Player.instance.questList[i].questName 
             && Player.instance.questList[i].questStatus == Quest.QuestStatus.Accepted)
            {
                switch (questType)
                {
                    case QuestType.Gathering:
                        if(Player.instance.itemAmount >= Player.instance.questList[i].requireAmount)
                        {
                            Player.instance.questList[i].questStatus = Quest.QuestStatus.Completed;
                            QuestManager.instance.UpdateQuestList();
                            Debug.Log("UPDATE");
                        }
                        break;

                    case QuestType.Talk:
                        if(hasTalked)
                        {
                            Player.instance.questList[i].questStatus = Quest.QuestStatus.Completed;
                            QuestManager.instance.UpdateQuestList();
                        }
                        break;

                    case QuestType.Reach:
                        if(hasReach)
                        {
                            Player.instance.questList[i].questStatus = Quest.QuestStatus.Completed;
                            QuestManager.instance.UpdateQuestList();
                        }
                        break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {

            for(int i = 0; i < Player.instance.questList.Count; i++)
            {
                if(Player.instance.questList[i].questName == questName)
                {
                    if (questType == QuestType.Reach)
                    {
                        hasReach = true;
                        CheckQuestIsComplete();
                    } 
                }
            }
        }
    }
}
