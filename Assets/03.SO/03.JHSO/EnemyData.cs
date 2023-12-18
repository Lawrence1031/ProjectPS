using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Àû Å¸ÀÔ
/// </summary>
public enum EnemyType
{
    Zombie,
}

public enum Target
{
    Player,
}

[CreateAssetMenu(fileName = "_enemy", menuName = "SO Menu/EnemyObj", order = 3)]
public class EnemyData : ScriptableObject
{
    [Header("info")]
    public string enemyName;
    public string enemyDesciption;
    public EnemyType enemyType;
    public Target target;
    public float Speed;

}
