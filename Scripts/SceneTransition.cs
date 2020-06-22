using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string sceneName;//场景转换的场景名字
    [SerializeField] public string password;//离开场景，赋值给人物的「密码」这个「密码」将会在【Entrance脚本】中被使用到

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            //MARKER 场景转换前保存所有NPC委派任务的当前状态
            #region 
            Questable[] questables = FindObjectsOfType<Questable>();
            if(questables != null)
            {
                foreach(Questable questable in questables)
                {
                    questable.SaveData();
                }
            }
            #endregion

            PlayerMovement.instance.scenePassword = password;
            SceneManager.LoadSceneAsync(sceneName);
        }
    }

}
