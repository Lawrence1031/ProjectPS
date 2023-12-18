using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject boardPrefab; // ����� ���� ������
    public int boardSize = 4;      // ���� �迭�� ũ�� (���⼭�� 4x4)
    public float spacing = 3.0f;   // �� ���� ���� ����

    void Start()
    {
        SpawnBoards(); // ���� �� ���� ����
    }

    void SpawnBoards()
    {
        List<GameObject> allBoards = new List<GameObject>(); // ��� ������ ������ ����Ʈ

        // ���� ������Ʈ�� ��ġ�� ���� ������ ���������� ���
        Vector3 basePosition = transform.position;

        // ���� �ݺ����� ����Ͽ� �׸��� ���·� ���� ����
        for (int x = 0; x < boardSize; x++)
        {
            for (int z = 0; z < boardSize; z++)
            {
                // ������ ��ġ ���
                Vector3 position = basePosition + new Vector3(x * spacing, 0, z * spacing);
                // ���� �������� �ν��Ͻ�ȭ�Ͽ� ����
                GameObject board = Instantiate(boardPrefab, position, Quaternion.identity, transform);
                allBoards.Add(board); // ������ ������ ����Ʈ�� �߰�
            }
        }

        // ������ ���� �� �Ϻθ� �������� ����
        SetRandomTraps(allBoards);
    }

    void SetRandomTraps(List<GameObject> boards)
    {
        int totalBoards = boardSize * boardSize;       // �� ������ ��
        int trapsToSet = totalBoards / 4;              // ��ü ���� �� 25%�� �������� ����

        Shuffle(boards); // ���� ����Ʈ�� �������� ����

        // ������ ����ŭ ���� ���� ����
        for (int i = 0; i < trapsToSet; i++)
        {
            TrapDamage trapDamage = boards[i].GetComponent<TrapDamage>();
            if (trapDamage != null)
            {
                trapDamage.isTrap = true; // �ش� ������ �������� ����
            }
        }
    }

    // ����Ʈ�� �������� ���� �޼ҵ�
    void Shuffle(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = Random.Range(i, list.Count); // ������ �ε��� ����
            list[i] = list[randomIndex];                   // ��ҵ��� ��ȯ�Ͽ� ����
            list[randomIndex] = temp;
        }
    }
}



