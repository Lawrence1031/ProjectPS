using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager2 : MonoBehaviour
{
    public GameObject boardPrefab; // 사용할 발판 프리팹
    public GameObject trapPrefab; // 사용할 발판 프리팹
    public int boardSize = 8;      // 발판 배열의 크기 (여기서는 8x8)
    public float spacing = 0f;   // 각 발판 간의 간격
    public float boardSizeFactor = 0f; // 발판 크기 배율

    private List<GameObject> allBoards = new List<GameObject>(); // 모든 발판을 저장할 리스트

    public bool IsInitialized { get; private set; } // 초기화 상태를 나타내는 프로퍼티

    void Start()
    {
        SpawnBoards(); // 게임 시작 시 발판 생성
        IsInitialized = true; // 모든 발판 생성 후 초기화 상태를 true로 설정
    }

    // 발판을 생성하는 메소드
    void SpawnBoards()
    {
        // 이 객체의 위치를 발판 생성의 기준점으로 사용
        Vector3 basePosition = transform.position;

        // boardSize에 지정된 크기만큼 이중 반복문을 사용하여 그리드 형태로 발판 생성
        for (int x = 0; x < boardSize; x++)
        {
            for (int z = 0; z < boardSize; z++)
            {
                // 각 발판의 월드 위치 계산
                Vector3 position = basePosition + new Vector3(x * spacing, 0, z * spacing);

                // 해당 위치가 함정 발판인지 일반 발판인지 결정
                // IsTrapPattern 메소드는 x, z 좌표에 따라 true(함정) 또는 false(일반) 반환
                GameObject prefabToUse = IsTrapPattern(x, z) ? trapPrefab : boardPrefab;

                // 계산된 위치에 발판 프리팹 인스턴스화하여 생성
                GameObject board = Instantiate(prefabToUse, position, Quaternion.identity, transform);
                board.transform.localScale *= boardSizeFactor; // 발판 크기 조정

                // 생성된 발판을 allBoards 리스트에 추가
                allBoards.Add(board);
            }
        }
    }

    // 지정된 패턴에 따라 함정 발판인지 판별
    bool IsTrapPattern(int x, int z)
    {
        // 각 줄(z)에 따라 함정(x)의 위치를 정의
        return (z == 0 && (x != 3)) ||
               (z == 1 && (x >= 1 && x != 3 && x <= 6)) ||
               (z == 2 &&  x == 2) ||
               (z == 3 && (x == 0 || (x >= 2 && x <= 6))) ||
               (z == 4 && (x == 0)) ||
               (z == 5 && (x <= 1 || (x >= 3 && x <= 4) || x >= 6)) ||
               (z == 6 && (x != 1 && x != 2 && x != 4 && x != 5)) ||
               (z == 7 && (x != 1));
    }

    // 모든 발판을 반환하는 메소드
    public List<GameObject> GetAllBoards()
    {
        return allBoards;
    }
}
