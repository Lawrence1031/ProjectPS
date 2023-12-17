using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public GameObject button;
    public GameObject door;

    /// <summary>
    /// 문을 열 수 있는 버튼의 능력을 추가하려고 합니다.
    /// </summary>
    public void PushDoorButton()
    {
        door.SetActive(false);        
    }
}
