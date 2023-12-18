using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;          // 플레이어의 이동 속도
    private Vector2 curMovementInput;// 현재 이동 입력 값
    public float jumpForce;          // 점프 힘
    public LayerMask groundLayerMask;// 바닥 감지를 위한 레이어 마스크

    [Header("Look")]
    public Transform cameraContainer;// 카메라를 담고 있는 컨테이너(일반적으로 플레이어의 머리 부분)
    public float minXLook;           // 카메라의 X축 최소 회전 각도 (아래로 보는 최대 각도)
    public float maxXLook;           // 카메라의 X축 최대 회전 각도 (위로 보는 최대 각도)
    private float camCurXRot;        // 현재 카메라의 X축 회전 값
    public float lookSensitivity;    // 카메라 조작의 민감도

    private Vector2 mouseDelta;      // 마우스 이동 값

    [HideInInspector]
    public bool canLook = true;      // 카메라 조작 가능 여부

    private Rigidbody _rigidbody;    // 게임 오브젝트에 부착된 Rigidbody 컴포넌트

    public static PlayerController instance; // 클래스의 싱글톤 인스턴스
    private void Awake()
    {
        instance = this;             // 싱글톤 인스턴스 설정
        _rigidbody = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 게임 시작 시 커서 잠금 (커서가 화면에서 사라짐)
    }

    private void FixedUpdate()
    {
        Move(); // 플레이어 이동 처리
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook(); // 카메라 시점 조정
        }
    }

    private void Move()
    {
        // 플레이어 이동 방향 및 속도 계산
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y; // 현재 수직 속도 유지 (중력 영향)
        _rigidbody.velocity = dir; // 계산된 속도 적용
    }

    void CameraLook()
    {
        // 마우스 입력에 따라 카메라 회전 값 계산 및 적용
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>(); // 마우스 이동 값 받기
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        // 이동 입력 처리 (WASD 또는 화살표 키)
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero; // 입력이 취소되면 이동 중지
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        // 점프 입력 처리 (일반적으로 스페이스바)
        if (context.phase == InputActionPhase.Started)
        {
            if (IsGrounded())
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse); // 점프 힘 적용
        }
    }

    private bool IsGrounded()
    {
        // 플레이어가 바닥에 닿아 있는지 감지
        Ray[] rays = new Ray[4]
        {
            // 플레이어 주변의 네 지점에서 아래 방향으로 레이를 발사
            new Ray(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.01f) , Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f)+ (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true; // 하나라도 바닥과 충돌하면 참 반환
            }
        }

        return false; // 모든 레이가 바닥과 충돌하지 않으면 거짓 반환
    }

    private void OnDrawGizmos()
    {
        // 에디터에서 시각적 도움을 위해 Gizmos 그리기
        Gizmos.color = Color.red;
        // 바닥 감지를 위한 레이 시각화
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down);
    }

    public void ToggleCursor(bool toggle)
    {
        // 마우스 커서 상태 토글
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle; // 커서 상태에 따라 카메라 조작 가능 여부 설정
    }
}

