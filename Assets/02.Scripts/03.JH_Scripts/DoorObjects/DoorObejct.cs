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
    /// Door ?ㅻ툕?앺듃???대쫫 ?쒖떆
    /// </summary>
    /// <returns></returns>
    public string GetInteractPrompt()
    {
        return string.Format("{0}", DoorData.displayName);
    }

    /// <summary>
    /// door????곹샇?묒슜
    /// </summary>
    public void OnInteract()
    {
        // door open
        //임시
       gameObject.SetActive(false);

       CinemachineController.Instance.OnChangedCineMachinePriority(aisleViCamera.Name, playerViCamera.Name);

       Invoke("InvokeController", 5f);

       // StartCoroutine(DealayCoroutineController());

    }


    private void InvokeController()
    {
        CinemachineController.Instance.OnChangedCineMachinePriority(playerViCamera.Name, aisleViCamera.Name);
        Debug.Log("인보크 작동");
        //aisleViCamera.gameObject.SetActive(false);
    }

    //private IEnumerator DealayCoroutineController()
    //{
    //    yield return new WaitForSecondsRealtime(5f);

    //    aisleViCamera.gameObject.SetActive(false);


    //    Debug.Log("돌아와");

    //}

}
