using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CinemachineController : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cinViCameraList;
    public Dictionary<string, CinemachineVirtualCamera> cinViCameraDictionary;

    public static CinemachineController Instance; // 싱글톤 인스턴스
   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있다면 중복 생성 방지
        }
    }


    private void Start()
    {
        cinViCameraDictionary = new Dictionary<string, CinemachineVirtualCamera>();

        // 딕셔너리에 key값 add
        foreach(CinemachineVirtualCamera virtualCamera in cinViCameraList)
        {
            cinViCameraDictionary.Add(virtualCamera.Name, virtualCamera);
        }

    }

    /// <summary>
    /// 카메라 체인지 매개변수 1 -> 바꿀 카메라, 매개변수 2  -> 현재 카메라 
    /// </summary>
    public void OnChangedCineMachinePriority(string targetVirtualCamera, string curVirtualCamera)
    {
        if(cinViCameraDictionary.ContainsKey(targetVirtualCamera) && cinViCameraDictionary.ContainsKey(curVirtualCamera))
        {
            CinemachineVirtualCamera _targetVirtualCamera = cinViCameraDictionary[targetVirtualCamera];
            CinemachineVirtualCamera _curVirtualCamera = cinViCameraDictionary[curVirtualCamera];

            //_targetVirtualCamera 우선순위
            _targetVirtualCamera.Priority = 11;
            _curVirtualCamera.Priority = 10;

        }


    }

}
