using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject boardPrefab; // 사용할 발판 프리팹
    public int boardSize = 4;      // 발판 배열의 크기 (여기서는 4x4)
    public float spacing = 3.0f;   // 각 발판 간의 간격

    void Start()
    {
        SpawnBoards(); // 시작 시 발판 생성
    }

    void SpawnBoards()
    {
        List<GameObject> allBoards = new List<GameObject>(); // 모든 발판을 저장할 리스트

        // 현재 오브젝트의 위치를 발판 생성의 기준점으로 사용
        Vector3 basePosition = transform.position;

        // 이중 반복문을 사용하여 그리드 형태로 발판 생성
        for (int x = 0; x < boardSize; x++)
        {
            for (int z = 0; z < boardSize; z++)
            {
                // 발판의 위치 계산
                Vector3 position = basePosition + new Vector3(x * spacing, 0, z * spacing);
                // 발판 프리팹을 인스턴스화하여 생성
                GameObject board = Instantiate(boardPrefab, position, Quaternion.identity, transform);
                allBoards.Add(board); // 생성된 발판을 리스트에 추가
            }
        }

        // 생성된 발판 중 일부를 함정으로 설정
        SetRandomTraps(allBoards);
    }

    void SetRandomTraps(List<GameObject> boards)
    {
        int totalBoards = boardSize * boardSize;       // 총 발판의 수
        int trapsToSet = totalBoards / 4;              // 전체 발판 중 25%를 함정으로 설정

        Shuffle(boards); // 발판 리스트를 무작위로 섞음

        // 지정된 수만큼 함정 발판 설정
        for (int i = 0; i < trapsToSet; i++)
        {
            TrapDamage trapDamage = boards[i].GetComponent<TrapDamage>();
            if (trapDamage != null)
            {
                trapDamage.isTrap = true; // 해당 발판을 함정으로 설정
            }
        }
    }

    // 리스트를 무작위로 섞는 메소드
    void Shuffle(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = Random.Range(i, list.Count); // 무작위 인덱스 선택
            list[i] = list[randomIndex];                   // 요소들을 교환하여 섞음
            list[randomIndex] = temp;
        }
    }
}



