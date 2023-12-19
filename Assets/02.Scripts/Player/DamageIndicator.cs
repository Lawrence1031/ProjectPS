using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image; // 피해 표시에 사용될 UI 이미지
    public float flashSpeed; // 플래시 효과의 속도

    private Coroutine coroutine; // 현재 실행 중인 코루틴을 추적하기 위한 변수

    // Flash 메소드는 피해 표시를 활성화하고 점차 사라지게 하는 역할을 함
    public void Flash()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine); // 이미 실행 중인 코루틴이 있다면 중단
        }

        image.enabled = true; // 이미지를 활성화
        image.color = Color.red; // 이미지의 색상을 빨간색으로 설정
        coroutine = StartCoroutine(FadeAway()); // FadeAway 코루틴 시작
    }

    // FadeAway 코루틴은 이미지의 투명도를 점차 감소시켜 피해 표시를 사라지게 함
    private IEnumerator FadeAway()
    {
        float startAlpha = 0.3f; // 시작 투명도 값
        float a = startAlpha; // 현재 투명도를 추적하는 변수

        // 투명도가 0이 될 때까지 반복
        while (a > 0.0f)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime; // 시간에 따라 투명도 감소
            image.color = new Color(1.0f, 0.0f, 0.0f, a); // 새 투명도로 이미지 색상 업데이트
            yield return null; // 다음 프레임까지 기다림
        }

        image.enabled = false; // 투명도가 0이 되면 이미지 비활성화
    }
}

