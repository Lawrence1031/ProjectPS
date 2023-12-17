using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour
{
   public Camera cameraToActivate;

    private void Start()
    {
        DeactivateAllCameras();
        cameraToActivate.gameObject.SetActive(true);
    }

    /// <summary>
    /// ��Ȱ��ȭ
    /// </summary>
    void DeactivateAllCameras()
    {
        Camera[] allCameras = Camera.allCameras;
        foreach (Camera cam in allCameras)
        {
            cam.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ī�޶� Ȱ��ȭ
    /// </summary>
    /// <param name="camera"></param>
    public void ActivateCamera(Camera camera)
    {
        DeactivateAllCameras();
        camera.gameObject.SetActive(true);
    }

}
