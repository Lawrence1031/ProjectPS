using Cinemachine;
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
    private Animator _moveAnimator;
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

    public GameObject inventoryWindow;  // 인벤토리 창
    public GameObject pauseWindow;      // 일시정지 창

    [HideInInspector]
    public bool canLook = true;
    public bool canMove = true;
    public bool isJump = false;

    private Rigidbody _rigidbody;// 플레이어 본체 확인 용

    public Camera _camera;
    //public CinemachineVirtualCamera startCamera;
    //public CinemachineVirtualCamera playerCamera;


    private PlayerConditions condition; //컨디션 받아오기

    public Button continueButton;// 계속하기 버튼
    public Button quitButton; //종료버튼
    public Button respownButton; //리스폰 버튼

    public GameObject deathWindow; //사망 화면

    public SaveData saveData;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        saveData = GetComponent<SaveData>();
        _moveAnimator = GetComponentInChildren<Animator>();
        condition = GetComponent<PlayerConditions>();
    }

    private void Start()
    {

        _camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        //saveData.LoadPlayerPosition();

        //CinemachineController.Instance.OnChangedCineMachinePriority(playerCamera.name, startCamera.name, true);

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
        if (canMove)
        {
            CameraLook();
        }

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && inventoryWindow.activeInHierarchy == false && deathWindow.activeInHierarchy == false)
        {
            Pause();
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

    public void Pause()
    {
        ToggleCursor(true);
        Time.timeScale = 0.0f;
        pauseWindow.SetActive(true);
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


    public void DeathScene()
    {
        deathWindow.SetActive(true);
        Time.timeScale = 0.0f;
        ToggleCursor(true);
    }
    public void Respown()
    {
        condition.isDead = false;
        condition.health.curValue = condition.health.maxValue;
        condition.UpdateHealthUI();
        saveData.LoadPlayerPosition();
        deathWindow.SetActive(false);
        Time.timeScale = 1.0f;
        ToggleCursor(false);
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
            _moveAnimator.SetBool("IsWalk", true);
            _curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _moveAnimator.SetBool("IsWalk", false);
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
            new Ray(transform.position + (transform.forward * 0.3f) + (Vector3.up * -0.1f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.3f) + (Vector3.up * -0.1f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.3f) + (Vector3.up * -0.1f), Vector3.down),
            new Ray(transform.position +(-transform.right * 0.3f) +(Vector3.up * -0.1f), Vector3.down),
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
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f) + (Vector3.up * -0.01f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f) + (Vector3.up * -0.01f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f) + (Vector3.up * -0.01f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f) + (Vector3.up * -0.01f), Vector3.down);
    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canMove = !toggle;
    }

    public void SetCanMove(bool move)
    {
        canMove = move;
    }

}
