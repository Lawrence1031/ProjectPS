using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootManager : MonoBehaviour
{
    public GameObject trapPrefab; // 사용할 함정 프리팹
    public int trapCount = 3;     // 배치할 함정의 수
    public Vector2 floorScale = new Vector2(2f, 2f); // 바닥 오브젝트의 스케일

    void Start()
    {
        SpawnTraps(); // 게임 시작 시 함정 생성
    }

    void SpawnTraps()
    {
        Vector2 floorSize = floorScale * 10f; // 바닥 오브젝트의 실제 크기 계산
        Vector3 basePosition = transform.position; // FootManager 오브젝트의 현재 위치를 기준점으로 사용

        for (int i = 0; i < trapCount; i++)
        {
            // FootManager 기준점을 포함하여 랜덤 위치를 생성
            Vector3 randomPosition = basePosition + new Vector3(
                Random.Range(-floorSize.x / 2, floorSize.x / 2), // X축 랜덤 위치
                -12.7f, // Y축 위치 (지정된 높이에 설정)
                Random.Range(-floorSize.y / 2, floorSize.y / 2)  // Z축 랜덤 위치
            );

            // 함정 프리팹을 인스턴스화하여 랜덤 위치에 배치
            Instantiate(trapPrefab, randomPosition, Quaternion.identity);
        }
    }
}



