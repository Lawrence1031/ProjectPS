using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootManager : MonoBehaviour
{
    public GameObject trapPrefab; // ����� ���� ������
    public int trapCount = 3;     // ��ġ�� ������ ��
    public Vector2 floorScale = new Vector2(2f, 2f); // �ٴ� ������Ʈ�� ������

    void Start()
    {
        SpawnTraps(); // ���� ���� �� ���� ����
    }

    void SpawnTraps()
    {
        Vector2 floorSize = floorScale * 10f;

        for (int i = 0; i < trapCount; i++)
        {
            // �ٴ� ������Ʈ�� ũ�⸦ �������� ���� ��ġ�� ����
            Vector3 randomPosition = new Vector3(
                Random.Range(-floorSize.x / 2, floorSize.x / 2), // X�� ���� ��ġ
                0.5f, // Y�� ��ġ (�ٴڿ� �����ϵ��� ����)
                Random.Range(-floorSize.y / 2, floorSize.y / 2)  // Z�� ���� ��ġ
            );

            // ���� �������� �ν��Ͻ�ȭ�Ͽ� ���� ��ġ�� ��ġ
            Instantiate(trapPrefab, randomPosition, Quaternion.identity, transform);
        }
    }
}


