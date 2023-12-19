using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;

public class BoardHint : MonoBehaviour
{
    public BoardManager boardManager; // BoardManager 참조
    public GameObject hintPrefab;     // 힌트를 표시할 프리팹 참조
    public GameObject switchObject;   // 활성화할 오브젝트 참조
    private PlayerController playerController; // 플레이어 컨트롤러 참조
    private bool hintsGenerated = false;       // 힌트 생성 여부 추적

    void Start()  // 시작 시 호출되는 메소드
    {
        // PlayerController 컴포넌트 찾기
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController 컴포넌트를 못찾음");
        }
        else
        {
            // 플레이어의 'Interact' 입력에 대한 콜백 메소드를 설정?
            playerController.GetComponent<PlayerInput>().actions["Interact"].performed += ctx => TryGenerateHints();
        }
    }

    // 힌트 생성을 시도하는 메소드
    void TryGenerateHints()
    {
        // switchObject가 활성화되어 있고, 아직 힌트가 생성되지 않았다면 힌트 생성 시작
        if (switchObject != null && switchObject.activeInHierarchy && !hintsGenerated)
        {
            // 힌트를 생성할 준비가 완료될 때까지 대기하는 코루틴 시작
            StartCoroutine(GenerateHintsWhenReady());
        }
    }

    // 힌트 생성 준비가 완료될 때까지 대기하는 코루틴
    IEnumerator GenerateHintsWhenReady()
    {
        // BoardManager의 초기화가 완료될 때까지 대기
        yield return new WaitUntil(() => boardManager.IsInitialized);

        // 힌트 생성
        GenerateHints();
    }

    // 힌트를 생성하는 메소드
    void GenerateHints()
    {
        // 힌트가 아직 생성되지 않았다면
        if (!hintsGenerated)
        {
            // BoardManager에서 관리하는 모든 발판에 대해 힌트 생성
            foreach (GameObject board in boardManager.GetAllBoards())
            {
                // 간격을 30%로 조정하기 위해 BoardManager와의 상대 위치를 계산하고 0.3배 함.
                // 이는 힌트를 BoardManager가 생성한 발판에 비례하여 축소
                Vector3 hintPosition = transform.position + (board.transform.position - boardManager.transform.position) * 0.3f;

                // 계산된 위치에 힌트 프리팹의 인스턴스 생성.
                GameObject hint = Instantiate(hintPrefab, hintPosition, Quaternion.identity, transform);

                // 힌트의 크기를 원래의 30%로 조정.
                hint.transform.localScale *= 0.3f;

                // 해당 발판이 함정인지 확인.
                TrapDamage trapDamage = board.GetComponent<TrapDamage>();
                if (trapDamage != null && trapDamage.isTrap)
                {
                    // 함정 발판이라면 힌트의 색상을 붉은색으로 변경.
                    hint.GetComponent<Renderer>().material.color = Color.red;
                }
            }
            // 모든 힌트 생성 후, BoardHint 오브젝트의 Z축 회전을 -90도로 설정
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -90);
            // 힌트 생성 상태 업데이트
            hintsGenerated = true;
        }
    }
}

