using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;          // �÷��̾��� �̵� �ӵ�
    private Vector2 curMovementInput;// ���� �̵� �Է� ��
    public float jumpForce;          // ���� ��
    public LayerMask groundLayerMask;// �ٴ� ������ ���� ���̾� ����ũ

    [Header("Look")]
    public Transform cameraContainer;// ī�޶� ��� �ִ� �����̳�(�Ϲ������� �÷��̾��� �Ӹ� �κ�)
    public float minXLook;           // ī�޶��� X�� �ּ� ȸ�� ���� (�Ʒ��� ���� �ִ� ����)
    public float maxXLook;           // ī�޶��� X�� �ִ� ȸ�� ���� (���� ���� �ִ� ����)
    private float camCurXRot;        // ���� ī�޶��� X�� ȸ�� ��
    public float lookSensitivity;    // ī�޶� ������ �ΰ���

    private Vector2 mouseDelta;      // ���콺 �̵� ��

    [HideInInspector]
    public bool canLook = true;      // ī�޶� ���� ���� ����

    private Rigidbody _rigidbody;    // ���� ������Ʈ�� ������ Rigidbody ������Ʈ

    public static PlayerController instance; // Ŭ������ �̱��� �ν��Ͻ�
    private void Awake()
    {
        instance = this;             // �̱��� �ν��Ͻ� ����
        _rigidbody = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ ��������
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ���� ���� �� Ŀ�� ��� (Ŀ���� ȭ�鿡�� �����)
    }

    private void FixedUpdate()
    {
        Move(); // �÷��̾� �̵� ó��
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook(); // ī�޶� ���� ����
        }
    }

    private void Move()
    {
        // �÷��̾� �̵� ���� �� �ӵ� ���
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y; // ���� ���� �ӵ� ���� (�߷� ����)
        _rigidbody.velocity = dir; // ���� �ӵ� ����
    }

    void CameraLook()
    {
        // ���콺 �Է¿� ���� ī�޶� ȸ�� �� ��� �� ����
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>(); // ���콺 �̵� �� �ޱ�
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        // �̵� �Է� ó�� (WASD �Ǵ� ȭ��ǥ Ű)
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero; // �Է��� ��ҵǸ� �̵� ����
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        // ���� �Է� ó�� (�Ϲ������� �����̽���)
        if (context.phase == InputActionPhase.Started)
        {
            if (IsGrounded())
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse); // ���� �� ����
        }
    }

    private bool IsGrounded()
    {
        // �÷��̾ �ٴڿ� ��� �ִ��� ����
        Ray[] rays = new Ray[4]
        {
            // �÷��̾� �ֺ��� �� �������� �Ʒ� �������� ���̸� �߻�
            new Ray(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.01f) , Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f)+ (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true; // �ϳ��� �ٴڰ� �浹�ϸ� �� ��ȯ
            }
        }

        return false; // ��� ���̰� �ٴڰ� �浹���� ������ ���� ��ȯ
    }

    private void OnDrawGizmos()
    {
        // �����Ϳ��� �ð��� ������ ���� Gizmos �׸���
        Gizmos.color = Color.red;
        // �ٴ� ������ ���� ���� �ð�ȭ
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down);
    }

    public void ToggleCursor(bool toggle)
    {
        // ���콺 Ŀ�� ���� ���
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle; // Ŀ�� ���¿� ���� ī�޶� ���� ���� ���� ����
    }
}

