using System.Collections.Generic;
using UnityEngine;

//这个脚本添加在Player游戏对象上
public class Player : MonoBehaviour
{
    public static Player instance;

    public int exp, gold, itemAmount;

    public List<Quest> questList = new List<Quest>();
    //public Dictionary<string, Quest> questDictionary = new Dictionary<string, Quest>();//OPTIONAL

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

}
