using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [Header("FootManager")]
    public int damagePerSecond = 30; // 초당 피해량
    [Header("BoardManager")]
    public bool isTrap = false;      // 이 발판이 함정인지 여부를 결정하는 플래그
    public int damageAmount = 100;   // 플레이어에게 입힐 피해량
    public float damageDelay = 1.0f; // 피해를 입히는데까지 걸리는 시간

    private PlayerConditions targetPlayerConditions; // 플레이어 조건 참조를 저장하기 위한 변수

    private void OnTriggerStay(Collider other)
    {
        // 플레이어가 FootManager 함정 영역 안에 있을 때 처리
        if (other.CompareTag("Player"))
        {
            PlayerConditions player = other.GetComponent<PlayerConditions>();
            if (player != null)
            {
                // 초당 피해 적용
                player.TakePhysicalDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어가 BoardManager 함정 발판과 충돌할 때 처리
        if (collision.gameObject.CompareTag("Player") && isTrap)
        {
            targetPlayerConditions = collision.gameObject.GetComponent<PlayerConditions>();
            if (targetPlayerConditions != null)
            {
                // 발판을 비활성화하고 지연 피해 적용
                gameObject.SetActive(false);
                targetPlayerConditions.DelayedDamage(damageAmount, damageDelay);
            }
            isTrap = false; // 함정을 비활성화하여 중복 피해를 방지
        }
    }
}

