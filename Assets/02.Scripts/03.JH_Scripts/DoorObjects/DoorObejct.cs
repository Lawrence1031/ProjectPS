using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObejct : MonoBehaviour, IInteraction
{
    public DoorData DoorData;

    /// <summary>
    /// Door ������Ʈ�� �̸� ǥ��
    /// </summary>
    /// <returns></returns>
    public string GetInteractPrompt()
    {
        return string.Format("{0}", DoorData.displayName);
    }

    /// <summary>
    /// door���� ��ȣ�ۿ�
    /// </summary>
    public void OnInteract()
    {
        // door open
    }
}
