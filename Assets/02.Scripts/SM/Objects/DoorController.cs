using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    private bool isOpen = false;
    public GameObject Door;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleDoor();
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
            transform.localScale = new Vector3(3, 3, 1);
            transform.position = new Vector3(0, 3f, 50);
            //Door.SetActive(false);
        }
        else
        {
            Debug.Log("Door is Closed");
            transform.localScale = new Vector3(10, 8, 1);
            transform.position = new Vector3(0, 3f, 50);
            //Door.SetActive(true);
        }

        isOpen = !isOpen;
    }
}
