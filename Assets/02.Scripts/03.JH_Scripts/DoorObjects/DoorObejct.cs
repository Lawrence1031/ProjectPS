using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using Unity.VisualScripting.Antlr3.Runtime;
using static UnityEditor.Progress;
using TMPro;

public class DoorObejct : MonoBehaviour, IInteraction
{
    public CinemachineVirtualCamera playerViCamera;
    public CinemachineVirtualCamera aisleViCamera;

    public PlayerController playerController;
    public DoorData doorData;
    public GameObject Door;
    public ItemData KeyObj;

    public bool needKey = false;
    public bool isOpen = false;
    private bool playerHasKey = false;

    private Quaternion initialRotation;
    private Quaternion targetRotation;


    public static DoorObejct instance;
    private void Start()
    {
        initialRotation = transform.rotation;
        targetRotation = initialRotation * Quaternion.Euler(0, 90, 0);
    }

    /// <summary>
    /// Door 이름
    /// </summary>
    /// <returns></returns>
    public string GetInteractPrompt()
    {
        if (doorData.displayName != null)
        {
            return string.Format("{0}", doorData.displayName);
        }

        return " ";
    }

    public string GetInteratHint()
    {
        if (doorData.needKeyName != null)
        {
            return string.Format("{0}", doorData.needKeyName);
        }

        return " ";
    }

    /// <summary>
    /// Door 상호작용
    /// </summary>
    public void OnInteract()
    {

        if (needKey)
        {
            if (isOpen)
            {
                SoundManager.instance.PlayDoorOpenEffect();
                ToggleDoor();
            }
            else
            {
                if (PlayerHasKey(KeyObj))
                {
                    Inventory.instance.RemoveItem(KeyObj);
                    SoundManager.instance.PlayDoorOpenEffect();
                    ToggleDoor();
                    needKey = false;
                }
                else
                {
                    SoundManager.instance.PlayDoorLockEffect(); 

                    if (playerController != null)
                    {
                        playerController.SetCanMove(false);
                    }
                    InteractionManager.instance.SetHintPromptText();
                }
            }
        }
        else
        {
            if (!isOpen)
            {
                OpenDoor();
                SoundManager.instance.PlayDoorOpenEffect();
            }
            else
            {
                SoundManager.instance.PlayDoorOpenEffect();
                ToggleDoor();
            }
        }

        if (aisleViCamera != null && playerViCamera != null)
        {
            CinemachineController.Instance.OnChangedCineMachinePriority(aisleViCamera.Name, playerViCamera.Name, false);
            Invoke("InvokeController", 5f);
        }

        //    Invoke("InvokeController", 5f);
        //}

        // StartCoroutine(DealayCoroutineController());

    }

    private void InvokeController()
    {
        CinemachineController.Instance.OnChangedCineMachinePriority(playerViCamera.Name, aisleViCamera.Name, true);
        //Debug.Log("인보크 작동");

        //aisleViCamera.gameObject.SetActive(false);
    }

    //private IEnumerator DealayCoroutineController()
    //{
    //    yield return new WaitForSecondsRealtime(5f);

    //    aisleViCamera.gameObject.SetActive(false);


    //    Debug.Log("돌아와");

    //}

    public bool PlayerHasKey(ItemData item)
    {
        if (Inventory.instance.HasItems(item))
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
        if (!isOpen)
        {
            SoundManager.instance.PlayDoorOpenEffect();
            transform.rotation = targetRotation;
        }
        else
        {
            SoundManager.instance.PlayDoorOpenEffect();
            transform.rotation = initialRotation;
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
        transform.rotation = targetRotation;
        SoundManager.instance.PlayDoorOpenEffect();
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
        transform.rotation = targetRotation;
        SoundManager.instance.PlayDoorOpenEffect();
    }
}
