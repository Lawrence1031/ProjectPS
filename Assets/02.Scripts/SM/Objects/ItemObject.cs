using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData item;

    public string GetInteractPrompt()
    {
        return string.Format("pickup {0}", item.displayName);
    }

    public void OnInteract()
    {
        //Inventory.instance.AddItem(item);
        Destroy(gameObject);
    }
}
