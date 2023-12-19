using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [Header("FootManager")]
    public int damagePerSecond = 30; // 초당 피해량
    [Header("BoardManager")]
    public bool isTrap = false;      // 이 발판이 함정인지 여부를 결정하는 플래그
    [Header("DropManager")]
    public bool isDropTrap = false; // 드랍 함정인지 여부를 결정하는 플래그
    public int dropDamageAmount = 100; // 드랍 함정의 피해량

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
        // 플레이어 테그와 충돌시 처리
        if (collision.gameObject.CompareTag("Player"))
        {
            targetPlayerConditions = collision.gameObject.GetComponent<PlayerConditions>();
            if (targetPlayerConditions != null)
            {
                if (isTrap)
                {
                    gameObject.SetActive(false);
                    isTrap = false; // 함정을 비활성화하여 중복 피해를 방지
                }

                if (isDropTrap)
                {
                    // 드랍 함정의 피해 적용 후 파괴
                    targetPlayerConditions.TakePhysicalDamage(dropDamageAmount);
                    Destroy(gameObject); // 드랍 함정 오브젝트 파괴
                }
            }
        }
    }
}

