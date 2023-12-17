using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObejct : MonoBehaviour, IInteraction
{
    public DoorData DoorData;

    /// <summary>
    /// Door 오브젝트의 이름 표시
    /// </summary>
    /// <returns></returns>
    public string GetInteractPrompt()
    {
        return string.Format("{0}", DoorData.displayName);
    }

    /// <summary>
    /// door와의 상호작용
    /// </summary>
    public void OnInteract()
    {
        // door open
    }
}
