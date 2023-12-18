using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
///  힌트 오브젝트 클래스
/// </summary>
public class HintObject : MonoBehaviour, IInteraction
{
    public HintData hintData;
    public Camera _changeCamera;
    public Camera _playerCamera;
    public CinemachineVirtualCamera hintObjViCamera;
    public CinemachineVirtualCamera playerViCamera;

    /// <summary>
    /// 힌트 오브젝트의 displayName return
    /// </summary>
    /// <returns></returns>
    public string GetInteractPrompt()
    {
        return string.Format("{0}", hintData.displayName);
    }

    /// <summary>
    /// 힌트오브젝트와 상호작용했을 시 
    /// </summary>
    public void OnInteract()
    {
        //시네머신 카메라 호출 
        CinemachineController.Instance.OnChangedCineMachinePriority(hintObjViCamera.Name, playerViCamera.Name);

        // 카메라의 우선순위를 높인다.
        //hintObjViCamera.MoveToTopOfPrioritySubqueue();
        //hintObjViCamera.Priority = 11;
        //playerViCamera.Priority = 10;

        //CamerController cameraController = FindObjectOfType<CamerController>();

        //if (cameraController != null)
        //{
        //    Debug.Log("호출2");
        //    cameraController.ActivateCamera(_changeCamera);

        //}
    }

    public void NonInteract()
    {

        CinemachineController.Instance.OnChangedCineMachinePriority(playerViCamera.Name, hintObjViCamera.Name);

        //Debug.Log("????????");


        //hintObjViCamera.Priority = 10;
        //playerViCamera.Priority = 11;
        //playerViCamera.MoveToTopOfPrioritySubqueue();

        //CamerController cameraController = FindObjectOfType<CamerController>();

        //if (cameraController != null)
        //{
        //    Debug.Log("호출3");
        //    cameraController.ActivateCamera(_playerCamera);

        //}
    }

}
