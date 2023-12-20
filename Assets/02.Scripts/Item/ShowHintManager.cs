using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowHintManager : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    private GameObject curInteractGameObject;
    private IInteraction curInteraction;

    [SerializeField] private HintObject hintObject;

    public TextMeshProUGUI hintText;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    /// <summary>
    /// 레이를 쏴 앞 오브젝트를 가리킨다.
    /// </summary>
    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            // 정중앙에 레이를 쏨
            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject.CompareTag("Door") && DoorAction.instanse.isOpen == false)
                {
                    if (_camera.gameObject.activeSelf == false)
                    {
                        UnSetPromptText();
                        curInteractGameObject = hit.collider.gameObject;
                        curInteraction = hit.collider.GetComponent<IInteraction>();
                    }
                    else if (hit.collider.gameObject != curInteractGameObject)
                    {
                        curInteractGameObject = hit.collider.gameObject;
                        curInteraction = hit.collider.GetComponent<IInteraction>();
                        SetPromptText();
                        /// 여기까지가 레이를 쏘고 바라본 정도까지임 아래는 오브젝트들 마다 interaction이 달라야함
                    }
                }
            }
            else
            {
                if (_camera.gameObject.activeSelf == false)
                {
                    hintText.gameObject.SetActive(false);
                }

                ///평시에 null
                curInteractGameObject = null;
                curInteraction = null;
                hintText.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 텍스트 출력
    /// </summary>
    private void SetPromptText()
    {
        hintText.gameObject.SetActive(true);
        hintText.text = string.Format("{0}", curInteraction.GetInteractPrompt());
    }

    private void UnSetPromptText()
    {
        hintText.gameObject.SetActive(false);
        hintText.text = string.Empty;
    }

    /// <summary>
    /// 상호작용
    /// </summary>
    public void OnInteractInput(InputAction.CallbackContext callbackContext)
    {
        //Debug.Log("false? " + _playerController.isJump);
        if (callbackContext.phase == InputActionPhase.Started && curInteraction != null)
        {
            curInteraction.OnInteract();
            curInteractGameObject = null;
            curInteraction = null;
            hintText.gameObject.SetActive(false);
        }
    }

    public void NonInteraction(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            hintObject.NonInteract();
            curInteractGameObject = null;
            curInteraction = null;
            hintText.gameObject.SetActive(false);
        }
    }

}
