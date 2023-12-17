using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  ��Ʈ ������Ʈ Ŭ����
/// </summary>
public class HintObject : MonoBehaviour, IInteraction
{
    public HintData hintData;
    public Camera _changeCamera;
    public Camera _playerCamera;

    /// <summary>
    /// ��Ʈ ������Ʈ�� displayName return
    /// </summary>
    /// <returns></returns>
    public string GetInteractPrompt()
    {
        return string.Format("{0}", hintData.displayName);
    }

    /// <summary>
    /// ��Ʈ������Ʈ�� ��ȣ�ۿ����� �� 
    /// </summary>
    public void OnInteract()
    {
        CamerController cameraController = FindObjectOfType<CamerController>();
        
        if (cameraController != null)
        {
            Debug.Log("ȣ��2");
            cameraController.ActivateCamera(_changeCamera);

        }
    }

    public void NonInteract()
    {
        CamerController cameraController = FindObjectOfType<CamerController>();

        if (cameraController != null)
        {
            Debug.Log("ȣ��3");
            cameraController.ActivateCamera(_playerCamera);

        }
    }

}
