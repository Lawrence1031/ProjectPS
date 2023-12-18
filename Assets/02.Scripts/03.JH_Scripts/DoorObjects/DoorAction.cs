using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorAction : MonoBehaviour
{
    //Inventory playerInventory = Inventory.Instance;
    public DoorData DoorData;
    public GameObject Door;
    public bool needKey = false;
    private bool isOpen = true;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + new Vector3(0, 2.5f, 0);
    }
    public string GetInteractPrompt()
    {
        return string.Format("{0}", DoorData.displayName);
    }

    public void OnInteract()
    {
        gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (needKey)
            {
                if (isOpen || PlayerHasKey())
                {
                    ToggleDoor();
                }
                else
                {
                    Debug.Log("열쇠가 필요합니다");
                }
            }
            else
            {
                ToggleDoor();
            }
        }
    }

    private bool PlayerHasKey()
    {
        //if (playerInventory != null)
        //{
        //    return playerInventory.HasKey();
        //}
        return false;
    }


    /// <summary>
    /// 키가 있다면 문을 여는 기믹이 시작되게 해야합니다.
    /// </summary>
    public void ToggleDoor()
    {
        if (isOpen)
        {
            Debug.Log("Door is Opened");
            transform.position = targetPosition;
        }
        else
        {
            Debug.Log("Door is Closed");
            transform.position = initialPosition;
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
