using UnityEngine;

public class Entrance : MonoBehaviour
{
    public string entrancePassword;//保证玩家在进入新的场景，Player自身携带的密码和这个「entrancePW」一致，就会出现在新场景的【预设位置】

    private void Start()
    {
        if(PlayerMovement.instance.scenePassword == entrancePassword)
        {
            PlayerMovement.instance.transform.position = transform.position;
        }
        else
        {
            Debug.LogError("Wrong PW. Please Check your Scene name and Entrance password");
        }
    }
}
