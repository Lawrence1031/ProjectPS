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
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pressed E Key");
                doorAction.ToggleDoor();
            }
        }
    }
}
