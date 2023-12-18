using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonPush : MonoBehaviour
{
    public GameObject Door;
    public GameObject Button;

    public DoorAction doorAction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAction.OpenDoor();
        }
    }
}
