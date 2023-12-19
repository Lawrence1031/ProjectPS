using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public interface IDamagable

{
    void TakePhysicalDamage(float damageAmount);
    // 피해를 받는 기능을 정의하는 메소드
    // 매개변수: damageAmount - 받는 피해의 양을 나타낸다. 이 값은 일반적으로
    // 양의 수로 표현되며, 객체의 체력 등에 영향을 준다.
}

public class PlayerConditions : MonoBehaviour, IDamagable
{
    // Condition 클래스는 플레이어의 체력과 관련된 정보를 관리
    [System.Serializable]
    public class Condition
    {
        public float curValue;  // 현재 체력 값
        public float maxValue;  // 최대 체력 값
        public float startValue;// 시작 시 체력 값
        public Image uiBar;     // UI 상의 체력 바


        // 체력을 증가시키는 메소드
        public void Add(float amount) => curValue = Mathf.Min(curValue + amount, maxValue);

        // 체력을 감소시키는 메소드
        public void Subtract(float amount) => curValue = Mathf.Max(curValue - amount, 0.0f);

        // 현재 체력의 백분율을 반환하는 메소드
        public float GetPercentage() => curValue / maxValue;
    }

    public Condition health; // 플레이어의 체력 상태
    public UnityEvent onTakeDamage; // 피해를 받았을 때 발생하는 이벤트
    private bool isDead = false; // 플레이어의 사망 상태를 추적하는 플래그

    private float lastGroundedTime; // 마지막으로 바닥에 닿은 시간
    private float timeSinceLastGrounded; // 마지막으로 바닥에 닿은 이후의 시간
    private const float FallDamageThreshold = 2.0f; // 바닥을 감지하지 못하는 최대 시간 (초)
    public int fallDamage = 100; // 낙사 피해량

    private PlayerController playerController; // PlayerController 참조

    // 시작 시 호출되는 메소드
    void Start()
    {
        health.curValue = health.startValue; // 시작 시 체력을 초기값으로 설정
        UpdateHealthUI(); // UI를 업데이트

        playerController = GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController 컴포넌트 못찾음");
        }
    }

    // 매 프레임마다 호출되는 메소드
    void Update()
    {
        if (playerController != null && !playerController.IsGround())
        {
            // 플레이어가 바닥에 닿지 않은 경우, 경과 시간을 누적
            timeSinceLastGrounded += Time.deltaTime;
        }
        else
        {
            // 플레이어가 바닥에 닿은 경우, 경과 시간을 초기화
            timeSinceLastGrounded = 0;
        }

        // 낙사 피해 검사
        if (timeSinceLastGrounded > FallDamageThreshold)
        {
            // 경과 시간이 낙사 임계값을 초과한 경우, 낙사 피해 적용
            TakePhysicalDamage(fallDamage);
            timeSinceLastGrounded = 0; // 피해를 입힌 후 경과 시간을 초기화
        }
    }

    // 피해를 받는 인터페이스 메소드 구현
    public void TakePhysicalDamage(float damageAmount)
    {
        if (isDead) return; // 이미 사망한 경우 추가 피해를 무시

        health.Subtract(damageAmount); // 체력 감소
        UpdateHealthUI(); // UI 업데이트

        // 피해를 받을 때 onTakeDamage 이벤트 발생
        onTakeDamage?.Invoke();

        // 체력이 0 이하로 떨어지면 사망 처리
        if (health.curValue <= 0.0f && !isDead)
        {
            Die();
        }
    }

    // 체력 UI를 업데이트하는 메소드
    private void UpdateHealthUI()
    {
        if (health.uiBar != null)
        {
            health.uiBar.fillAmount = health.GetPercentage(); // 체력 바의 fillAmount를 업데이트
        }
    }

    // 사망 처리 메소드
    public void Die()
    {
        if (!isDead)
        {
            Debug.Log("플레이어가 죽었다.");
            isDead = true;

            // 임시로 현재 씬을 다시 로드하여 게임을 재시작
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // 지연된 피해 처리를 위한 코루틴
    public bool IsDead() => isDead;

    public void DelayedDamage(int damageAmount, float delay) => StartCoroutine(DelayDamage(damageAmount, delay));

    private IEnumerator DelayDamage(int damageAmount, float delay)
    {
        yield return new WaitForSeconds(delay); // 지정된 시간만큼 대기
        if (!isDead) TakePhysicalDamage(damageAmount); // 대기 후 피해 적용
    }
}


