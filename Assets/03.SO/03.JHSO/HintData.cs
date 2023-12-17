using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HintType
{
    Paper
}

[CreateAssetMenu(fileName = "_hint", menuName = "SO Menu/HintObj ", order = 1)]
public class HintData : ScriptableObject
{
    [Header("info")]
    public string displayName;
    public HintType hintType;

}
