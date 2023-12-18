using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class DoorObejct : MonoBehaviour, IInteraction
{
    public CinemachineVirtualCamera playerViCamera;
    public CinemachineVirtualCamera aisleViCamera;

    public DoorData DoorData;

    /// <summary>
    /// Door ������Ʈ�� �̸� ǥ��
    /// </summary>
    /// <returns></returns>
    public string GetInteractPrompt()
    {
        return string.Format("{0}", DoorData.displayName);
    }

    /// <summary>
    /// door���� ��ȣ�ۿ�
    /// </summary>
    public void OnInteract()
    {
        // door open
        //�ӽ�
       gameObject.SetActive(false);

       CinemachineController.Instance.OnChangedCineMachinePriority(aisleViCamera.Name, playerViCamera.Name);

       Invoke("InvokeController", 5f);

       // StartCoroutine(DealayCoroutineController());

    }


    private void InvokeController()
    {
        CinemachineController.Instance.OnChangedCineMachinePriority(playerViCamera.Name, aisleViCamera.Name);
        Debug.Log("�κ�ũ �۵�");
        //aisleViCamera.gameObject.SetActive(false);
    }

    //private IEnumerator DealayCoroutineController()
    //{
    //    yield return new WaitForSecondsRealtime(5f);

    //    aisleViCamera.gameObject.SetActive(false);


    //    Debug.Log("���ƿ�");

    //}

}
