using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorAction : MonoBehaviour
{
    public GameObject Door;
    private Position doorPosition;
    private bool isOpen = true;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + new Vector3(0, 2.5f, 0);
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Encounter");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pressed E Key");
                ToggleDoor();
            }
        }
    }



    /// <summary>
    /// 키가 있다면 문을 여는 기믹이 시작되게 해야합니다.
    /// </summary>
    public void ToggleDoor()
    {
        //if (hasKey)
        //{
        //    isOpen = true;
        //    transform.localScale = new Vector3(3, 3, 1);
        //    transform.position = new Vector3(0, 3f, 50);
        //}
        //else
        //{
        //    Debug.Log("You need a Key. Find the Key in this room");
        //}

        if (isOpen)
        {
            Debug.Log("Door is Opened");
            transform.position = targetPosition;
            //Door.SetActive(false);
        }
        else
        {
            Debug.Log("Door is Closed");
            transform.position = initialPosition;
            //Door.SetActive(true);
        }

        isOpen = !isOpen;
    }

    /// <summary>
    /// 문제를 해결했을 때 문이 열리게하는 함수입니다.
    /// public DoorAction doorAction; 로 데이터를 가져간 후에
    /// DoorAction.OpenDoor()로 문이 열리게 하면 됩니다.
    /// </summary>
    public void OpenDoor()
    {
        isOpen = true;
        transform.position = targetPosition;
    }
}
