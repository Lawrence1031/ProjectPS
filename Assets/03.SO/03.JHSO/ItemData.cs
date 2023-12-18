using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ������ ������
/// </summary>
public enum ItemType
{
    KeyItem,
    ClueItem,
    CorrectItem,
}

/// <summary>
/// �����ۿ� �ʿ��� ��ҵ�
/// </summary>
/// 
[CreateAssetMenu(fileName ="_item", menuName ="SO Menu/NewItem ", order = 0)]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    public bool IsRoomOpen;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;
}
