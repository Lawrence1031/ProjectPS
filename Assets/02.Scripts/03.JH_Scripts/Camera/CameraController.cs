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
    /// 비활성화
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
    /// 카메라 활성화
    /// </summary>
    /// <param name="camera"></param>
    public void ActivateCamera(Camera camera)
    {
        DeactivateAllCameras();
        camera.gameObject.SetActive(true);
    }

}
