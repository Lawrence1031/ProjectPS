using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  힌트 오브젝트 클래스
/// </summary>
public class HintObject : MonoBehaviour, IInteraction
{
    public HintData hintData;
    public Camera _changeCamera;
    public Camera _playerCamera;

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
        CamerController cameraController = FindObjectOfType<CamerController>();
        
        if (cameraController != null)
        {
            Debug.Log("호출2");
            cameraController.ActivateCamera(_changeCamera);

        }
    }

    public void NonInteract()
    {
        CamerController cameraController = FindObjectOfType<CamerController>();

        if (cameraController != null)
        {
            Debug.Log("호출3");
            cameraController.ActivateCamera(_playerCamera);

        }
    }

}
