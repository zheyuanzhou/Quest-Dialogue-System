using UnityEngine;

//这个脚本添加在所有【可收集】的物品游戏对象上
public class Item : MonoBehaviour
{
    private QuestTarget questTarget;

    private void Start()
    {
        questTarget = GetComponent<QuestTarget>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player.instance.itemAmount += 1;//接触到以后累加加一
            questTarget.CheckQuestIsComplete();
            Destroy(gameObject);
        }
    }
}
