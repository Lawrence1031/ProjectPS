using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [Header("FootManager")]
    public int damagePerSecond = 30; // �ʴ� ���ط�
    [Header("BoardManager")]
    public bool isTrap = false;      // �� ������ �������� ���θ� �����ϴ� �÷���
    public int damageAmount = 100;   // �÷��̾�� ���� ���ط�
    public float damageDelay = 1.0f; // ���ظ� �����µ����� �ɸ��� �ð�

    private PlayerConditions targetPlayerConditions; // �÷��̾� ���� ������ �����ϱ� ���� ����

    private void OnTriggerStay(Collider other)
    {
        // �÷��̾ FootManager ���� ���� �ȿ� ���� �� ó��
        if (other.CompareTag("Player"))
        {
            PlayerConditions player = other.GetComponent<PlayerConditions>();
            if (player != null)
            {
                // �ʴ� ���� ����
                player.TakePhysicalDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �÷��̾ BoardManager ���� ���ǰ� �浹�� �� ó��
        if (collision.gameObject.CompareTag("Player") && isTrap)
        {
            targetPlayerConditions = collision.gameObject.GetComponent<PlayerConditions>();
            if (targetPlayerConditions != null)
            {
                // ������ ��Ȱ��ȭ�ϰ� ���� ���� ����
                gameObject.SetActive(false);
                targetPlayerConditions.DelayedDamage(damageAmount, damageDelay);
            }
            isTrap = false; // ������ ��Ȱ��ȭ�Ͽ� �ߺ� ���ظ� ����
        }
    }
}

