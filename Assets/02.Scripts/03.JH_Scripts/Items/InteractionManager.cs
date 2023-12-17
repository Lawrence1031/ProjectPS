using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    private GameObject curInteractGameObject;
    private IInteraction curInteraction;
    
    [SerializeField] private HintObject hintObject;

    public TextMeshProUGUI promptText;

    private Camera _camera;

    private PlayerController _playerController;

    private void Start()
    {
        _camera = Camera.main;
        _playerController = GetComponent<PlayerController>();
    }

    /// <summary>
    /// ���̸� �� �� ������Ʈ�� ����Ų��.
    /// </summary>
    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            // ���߾ӿ� ���̸� ��
            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {

                if (_camera.gameObject.activeSelf == false)
                {
                    UnSetPromptText();
                    curInteractGameObject = hit.collider.gameObject;
                    curInteraction = hit.collider.GetComponent<IInteraction>();
                }
                else if (hit.collider.gameObject != curInteractGameObject)
                {
                    Debug.Log("camera" + _camera);

                    curInteractGameObject = hit.collider.gameObject;
                    curInteraction = hit.collider.GetComponent<IInteraction>();
                    SetPromptText();
                    /// ��������� ���̸� ��� �ٶ� ���������� �Ʒ��� ������Ʈ�� ���� interaction�� �޶����
                }
               
            }
            else
            {
                if (_camera.gameObject.activeSelf == false)
                {
                    promptText.gameObject.SetActive(false);
                }

                ///��ÿ� null
                curInteractGameObject = null;
                curInteraction = null;
                promptText.gameObject.SetActive(false);
            }

        }
    }

    /// <summary>
    /// �ؽ�Ʈ ���
    /// </summary>
    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = string.Format("<b>[E]</b> {0}", curInteraction.GetInteractPrompt());
    }

    private void UnSetPromptText()
    {
        promptText.gameObject.SetActive(false);
        promptText.text = string.Empty;
    }

    /// <summary>
    /// ��ȣ�ۿ�
    /// </summary>
    public void OnInteractInput(InputAction.CallbackContext callbackContext)
    {
        //Debug.Log("false? " + _playerController.isJump);
        if (callbackContext.phase == InputActionPhase.Started && curInteraction != null)
        {
            curInteraction.OnInteract();
            curInteractGameObject = null;
            curInteraction = null;
            promptText.gameObject.SetActive(false);
        }
    }

    public void NonInteraction(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started && _camera.gameObject.activeSelf == false)
        {
            hintObject.NonInteract();
            curInteractGameObject = null;
            curInteraction = null;
            promptText.gameObject.SetActive(false);
        }
    }

}
