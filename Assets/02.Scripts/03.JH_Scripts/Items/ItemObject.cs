using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteraction
{
    public ItemData itemData;

    public string GetInteractPrompt()
    {
        return string.Format("{0}", itemData.displayName);
    }

    public void OnInteract()
    {
        Destroy(gameObject);
    }
}
