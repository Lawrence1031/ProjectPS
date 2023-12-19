using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BoardHint : MonoBehaviour
{
    // BoardManager에 대한 참조.
    public BoardManager boardManager;
    // 힌트를 표시할 프리팹에 대한 참조.
    public GameObject hintPrefab;

    // 게임 시작 시 호출되는 코루틴.
    IEnumerator Start()
    {
        // BoardManager가 초기화될 때까지 대기.
        // 이는 BoardManager가 모든 발판을 생성하고 난 후 힌트를 생성
        yield return new WaitUntil(() => boardManager.IsInitialized);

        // 힌트 생성 메소드 호출.
        GenerateHints();
    }

    // 힌트를 생성하는 메소드.
    void GenerateHints()
    {
        // BoardManager에서 관리하는 모든 발판에 대해 반복.
        foreach (GameObject board in boardManager.GetAllBoards())
        {
            // 간격을 30%로 조정하기 위해 BoardManager와의 상대 위치를 계산하고 0.3배 함.
            // 이는 힌트를 BoardManager가 생성한 발판에 비례하여 축소
            Vector3 hintPosition = transform.position + (board.transform.position - boardManager.transform.position) * 0.2f;

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
        // 모든 힌트 생성이 끝난 후, BoardHint 오브젝트의 Z축 회전을 -90도로 설정.
        // 이는 힌트 오브젝트들이 특정 방향을 가르킴
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -90);
    }
}


