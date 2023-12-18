using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public GameObject Door;
    public GameObject Button;

    public DoorAction doorAction;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Encounter");
            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("Pressed T Key");
                doorAction.ToggleDoor();
            }
        }
    }
}
