using System.Collections;
using UnityEngine;

public class Talkable : MonoBehaviour
{
    [SerializeField] private bool isEntered;

    public bool hasName;//默认是没有名字的
    [TextArea(1, 5)] public string[] lines;
    [TextArea(1, 4)] public string[] congratsLines;
    [TextArea(1, 4)] public string[] completedLines;

    public GameObject talkIcon;//用来提示玩家可以对话的UI交互

    public Questable questable;//当前说话的NPC，是否含有可以委派任务的能力
    public QuestTarget questTarget;//这个脚本中并没有访问，但是在DM脚本中有使用到这个变量

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isEntered = true;

            talkIcon.SetActive(true);//开启的时候其实透明度还是等于0
            StartCoroutine(FadeIn());//渐变效果的调用

            DialogueManager.instance.talkable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isEntered = false;

            //StartCoroutine(FadeOut());//ERROR
            talkIcon.GetComponent<CanvasGroup>().alpha = 0;
            talkIcon.SetActive(false);

            DialogueManager.instance.talkable = null;
        }
    }

    private void Update()
    {
        if(isEntered && Input.GetKeyDown(KeyCode.W) && DialogueManager.instance.dialogueBox.activeInHierarchy == false)
        {
            if(questable == null)
            {
                DialogueManager.instance.ShowDialogue(lines, hasName);
                //Debug.Log("BOARD LINES");
            }
            else
            {
                if (questable.quest.questStatus == Quest.QuestStatus.Completed)
                {
                    DialogueManager.instance.ShowDialogue(completedLines, hasName);
                    //Debug.Log("COMPLETED LINES");
                }
                else
                {
                    DialogueManager.instance.ShowDialogue(lines, hasName);
                    //Debug.Log("NORMAL NPC LINES");
                }
            }
        }
    }

    IEnumerator FadeIn()
    {
        talkIcon.GetComponent<CanvasGroup>().alpha = 0;
        while (talkIcon.GetComponent<CanvasGroup>().alpha < 1)
        {
            talkIcon.GetComponent<CanvasGroup>().alpha += 0.02f;
            yield return null;
        }
    }

    //ERROR 这个会有一个问题，就是当Alpha的值还不是0的时候，人物已经出范围了
    //SOLVED 所以暂时改成了「瞬间消失」
    //IEnumerator FadeOut()
    //{
    //    while (talkIcon.GetComponent<CanvasGroup>().alpha > 0)
    //    {
    //        talkIcon.GetComponent<CanvasGroup>().alpha -= 0.03f;
    //        yield return null;
    //    }
    //    talkIcon.SetActive(false);
    //}



}
