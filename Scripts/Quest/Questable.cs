using UnityEngine;

//MARKER 每个【可以派发任务】的NPC都会添加这个脚本，路标等单方面功能性的游戏对象除外，不添加这个脚本，他们只需要添加talkable脚本
public class Questable : MonoBehaviour
{
    public Quest quest;//可委派的具体任务

    public bool isFinished;

    public QuestTarget questTarget;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        LoadData();
    }

    public void DelegateQuest()
    {
        if(isFinished == false)
        {
            if (quest.questStatus == Quest.QuestStatus.Waitting)
            {
                quest.questStatus = Quest.QuestStatus.Accepted;//初次委托时将任务更改为【接收】状态
                Player.instance.questList.Add(quest);

                if (quest.questType == Quest.QuestType.Gathering)
                {
                    questTarget.CheckQuestIsComplete();

                    #region
                    if(DialogueManager.instance.GetQuestResult() == true)
                    {
                        DialogueManager.instance.ShowDialogue(DialogueManager.instance.talkable.congratsLines, DialogueManager.instance.talkable.hasName);
                        isFinished = true;
                        OfferRewards();
                    }
                    #endregion
                }
            }
            else
            {
                Debug.Log(string.Format("QUEST: {0} has accepted already!", quest.questName));
            }
        }
        else
        {
            Debug.Log("You have Finished THIS QUEST BRO!");
        }

        QuestManager.instance.UpdateQuestList();
    }

    public void OfferRewards()
    {
        Player.instance.exp += quest.expReward;
        Player.instance.gold += quest.goldReward;
        QuestManager.instance.UpdateUIText();
        Debug.Log("$*$*$*****Bonus*****$*$*$");
    }

    //TODO 先简单处理一下「场景转换」过程中，NPC任务不能保存的情况
    //MARKER 这个方法将会在SceneTransition脚本中调用
    public void SaveData()
    {
        switch(quest.questStatus)
        {
            case Quest.QuestStatus.Waitting:
                PlayerPrefs.SetInt(quest.questName, (int)Quest.QuestStatus.Waitting);
                break;

            case Quest.QuestStatus.Accepted:
                PlayerPrefs.SetInt(quest.questName, (int)Quest.QuestStatus.Accepted);
                break;

            case Quest.QuestStatus.Completed:
                PlayerPrefs.SetInt(quest.questName, (int)Quest.QuestStatus.Completed);
                break;
        }

        switch(isFinished)
        {
            case true:
                PlayerPrefs.SetInt(quest.questName + " isFinished", 0);
                break;

            case false:
                PlayerPrefs.SetInt(quest.questName + " isFinished", 1);
                break;
        }
    }

    //MARKER 这个方法将会在Start方法中被调用
    //CORE hasKey的检测是必须要做的，因为游戏一开始的时候不存在任何的KEY，是肯定会出错的，需要检验
    public void LoadData()
    {
        if(PlayerPrefs.HasKey(quest.questName))
        {
            switch (PlayerPrefs.GetInt(quest.questName))
            {
                case 0:
                    quest.questStatus = Quest.QuestStatus.Waitting;
                    break;

                case 1:
                    quest.questStatus = Quest.QuestStatus.Accepted;
                    break;

                case 2:
                    quest.questStatus = Quest.QuestStatus.Completed;
                    break;
            }
        }
        else
        {
            Debug.LogWarning(string.Format("Preferences dont have this key: {0}", quest.questName));
        }

        if (PlayerPrefs.HasKey(quest.questName + " isFinished"))
        {
            switch (PlayerPrefs.GetInt(quest.questName + " isFinished"))
            {
                case 0:
                    isFinished = true;
                    break;

                case 1:
                    isFinished = false;
                    break;
            }
        }
        else
        {
            Debug.LogWarning(string.Format("Preferences dont have this key: {0}", quest.questName + " isFinished"));
        }
    }
}
