using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CinemachineController : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cinViCameraList;
    public Dictionary<string, CinemachineVirtualCamera> cinViCameraDictionary;

    public static CinemachineController Instance; // �̱��� �ν��Ͻ�
   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �ִٸ� �ߺ� ���� ����
        }
    }


    private void Start()
    {
        cinViCameraDictionary = new Dictionary<string, CinemachineVirtualCamera>();

        // ��ųʸ��� key�� add
        foreach(CinemachineVirtualCamera virtualCamera in cinViCameraList)
        {
            cinViCameraDictionary.Add(virtualCamera.Name, virtualCamera);
        }

    }

    /// <summary>
    /// ī�޶� ü���� �Ű����� 1 -> �ٲ� ī�޶�, �Ű����� 2  -> ���� ī�޶� 
    /// </summary>
    public void OnChangedCineMachinePriority(string targetVirtualCamera, string curVirtualCamera)
    {
        if(cinViCameraDictionary.ContainsKey(targetVirtualCamera) && cinViCameraDictionary.ContainsKey(curVirtualCamera))
        {
            CinemachineVirtualCamera _targetVirtualCamera = cinViCameraDictionary[targetVirtualCamera];
            CinemachineVirtualCamera _curVirtualCamera = cinViCameraDictionary[curVirtualCamera];

            //_targetVirtualCamera �켱����
            _targetVirtualCamera.Priority = 11;
            _curVirtualCamera.Priority = 10;

        }


    }

}
