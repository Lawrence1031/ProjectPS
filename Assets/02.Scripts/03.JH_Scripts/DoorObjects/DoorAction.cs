using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class DoorAction : MonoBehaviour, IInteraction
{
    //Inventory playerInventory = Inventory.Instance;
    public DoorData doorData;
    public GameObject Door;
    public ItemData KeyObj;

    public bool needKey = false;
    public bool isOpen = false;
    private bool playerHasKey;

    public Vector3 initialPosition;
    public Vector3 targetPosition;

    public static DoorAction instanse;

    private void Awake()
    {
        instanse = this;
    }

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + new Vector3(0, 2.5f, 0);
    }
    public string GetInteractPrompt()
    {
        return string.Format("{0}", doorData.displayName);
    }

    public string GetInteratHint()
    {
        return string.Format("{0}", doorData.needKeyName);
    }
    public void OnInteract()
    {
        if (needKey)
        {
            if (isOpen)
            {
                ToggleDoor();
            }
            else
            {
                if (Inventory.instance.HasItems(KeyObj) == true)
                {
                    OpenDoor();
                }
            }
        }
        else
        {
            ToggleDoor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (needKey)
            {
                if (isOpen)
                {
                    ToggleDoor();
                }
                else
                {

                }
            }
            else
            {
                ToggleDoor();
            }
        }
    }

    public bool PlayerHasKey(ItemData item)
    {
        if (Inventory.instance.HasItems(item) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// 키가 있다면 문을 여는 기믹이 시작되게 해야합니다.
    /// </summary>
    public void ToggleDoor()
    {
        if (isOpen)
        {
            transform.position = targetPosition;
        }
        else
        {
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

    /// <summary>
    /// 열쇠가 있을 때, 열쇠를 이용해서 문을 여는 함수입니다.
    /// DoorAction.OpenDoor(사용할 아이템 이름)으로 문이 열리게 하면 됩니다.
    /// </summary>
    /// <param name="item"></param>
    public void OpenDoorUseKey(ItemData item)
    {
        PlayerHasKey(item);
        isOpen = true;
        transform.position = targetPosition;
    }
}
