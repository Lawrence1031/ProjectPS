using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public GameObject button;
    public GameObject door;

    public void PushDoorButton()
    {
        door.SetActive(false);        
    }
}
