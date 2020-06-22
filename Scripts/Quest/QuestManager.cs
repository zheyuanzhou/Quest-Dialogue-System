using UnityEngine;
using UnityEngine.UI;

//MARKER 这个脚本将会放在【UI Canvas】游戏对象上
public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public GameObject[] questArray;

    public GameObject questPanel;

    public Text expText, goldText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateQuestList();
        questPanel.SetActive(false);
    }

    //MARKER 这个方法将会在【领取好任务】【任务完成后】调用
    public void UpdateQuestList()
    {
        //如果我们要将【完成的任务】移出UI任务列表，我们就不能这么遍历，而是遍历有多少UI任务栏 TODO
        for(int i = 0; i < Player.instance.questList.Count; i++)//有多少个任务显示多少个List，而不是有多少List显示多少个任务
        {
            questArray[i].transform.GetChild(0).GetComponent<Text>().text = Player.instance.questList[i].questName;

            if(Player.instance.questList[i].questStatus == Quest.QuestStatus.Accepted)
            {
                questArray[i].transform.GetChild(1).GetComponent<Text>().text
                = "<color=red>" + Player.instance.questList[i].questStatus + "</color>";
            }
            else if (Player.instance.questList[i].questStatus == Quest.QuestStatus.Completed)
            {
                questArray[i].transform.GetChild(1).GetComponent<Text>().text
                = "<color=lime>" + Player.instance.questList[i].questStatus + "</color>";
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && PlayerMovement.instance.isTalking == false)
        {
            questPanel.gameObject.SetActive(!questPanel.activeInHierarchy);
        }

        //SOLVED 修复：当开启【UI任务列表】时，和NPC开启对话【UI任务列表】还开启的问题
        if (PlayerMovement.instance.isTalking && questPanel.activeInHierarchy)
        {
            questPanel.gameObject.SetActive(false);
        }
    }

    public void UpdateUIText()
    {
        expText.text = "EXP: " + Player.instance.exp;
        goldText.text = "GOLD: " + Player.instance.gold;
    }
}
