using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// 플레이어의 컨트롤러
/// </summary>
public class PlayerController : MonoBehaviour
{
    // 이동 관련
    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;
    public LayerMask grounLayerMask;

    public Vector2 _curMovementInput;


    // 조준점
    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    public float lookSensitivity; // 민감도

    private float _camCurXRot;
    private Vector2 mouseDelta;

    public GameObject inventoryWindow;
    public GameObject pauseWindow;

    [HideInInspector]
    public bool canLook = true;
    public bool canMove = true;
    public bool isJump = false;

    private Rigidbody _rigidbody;

    public Camera _camera;

    private PlayerConditions condition;

    public Button continueButton;
    public Button quitButton;

    private SaveData saveData;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        saveData = GetComponent<SaveData>();
    }

    private void Start()
    {

        _camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        //saveData.LoadPlayerPosition();
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            Move();
        }
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && inventoryWindow.activeInHierarchy == false)
        {
            ToggleCursor(true);
            Time.timeScale = 0.0f;
            pauseWindow.SetActive(true);
        }
        Button btn = continueButton.GetComponent<Button>();
        btn.onClick.AddListener(Continue);

        Button qbtn = quitButton.GetComponent<Button>();
        qbtn.onClick.AddListener(Quit);
    }
    /// <summary>
    ///  움직일 때 필요한 값 할당
    /// </summary>
    private void Move()
    {
        // 앞으로 이동하면 inputaction에서 받아온 y를, 우측으로가면 inputaction에서 받아온 x축을 곱한다.
        Vector3 dir = transform.forward * _curMovementInput.y + transform.right * _curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    private void Continue()
    {
        Time.timeScale = 1.0f;
        pauseWindow.SetActive(false);
        ToggleCursor(false);
    }

    private void Quit()
    {
        Application.Quit();
        Debug.Log("게임이 종료가 된거임!!! 유니티 에디터에선 안꺼지는 거임!!!");
    }

    /// <summary>
    /// 마우스로 카메라를 움직임
    /// </summary>
    void CameraLook()
    {
        if (_camera.gameObject.activeSelf == false)
        {
            ToggleCursor(true);
        }
        else if (inventoryWindow.activeInHierarchy)
        {
            ToggleCursor(true);
        }
        else if (_camera.gameObject.activeSelf == true && inventoryWindow.activeInHierarchy == false)
        {
            ToggleCursor(false);
        }

        if (canLook)
        {
            _camCurXRot += mouseDelta.y * lookSensitivity;
            _camCurXRot = Mathf.Clamp(_camCurXRot, minXLook, maxXLook);
            cameraContainer.localEulerAngles = new Vector3(-_camCurXRot, 0, 0);

            transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
        }
    }


    /// <summary>
    /// 마우스 델타값으로 시야 조정 및 InputSystem 받아옴
    /// </summary>
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// InputSystem 으로 움직임 구현
    /// </summary>
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _curMovementInput = Vector2.zero;
        }

    }

    /// <summary>
    /// 점프 InputSystem
    /// </summary>
    /// <param name="context"></param>
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (IsGround())
            {
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
            }
        }
    }


    /// <summary>
    /// Ground 체크
    /// </summary>
    public bool IsGround()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (Vector3.up * -0.7f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (Vector3.up * -0.7f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (Vector3.up * -0.7f), Vector3.down),
            new Ray(transform.position +(-transform.right * 0.2f) +(Vector3.up * -0.7f), Vector3.down),
        };

        // 4중 하나라도 ground와 맞닿았다면
        for (int i = 0; i < rays.Length; i++)
        {
            //Debug.Log($"{i}" + rays[i]);

            if (Physics.Raycast(rays[i], 0.1f, grounLayerMask))
            {
                //Debug.Log("ray?");
                isJump = true;
                return true;
            }
        }


        return false;

    }

    /// <summary>
    /// Gizmos 그리기
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f) + (Vector3.up * -0.7f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f) + (Vector3.up * -0.7f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f) + (Vector3.up * -0.7f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f) + (Vector3.up * -0.7f), Vector3.down);
    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    public void SetCanMove(bool move)
    {
        canMove = move;
    }

}
