using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Key
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public ItemType type;
    public Sprite icon;
}
