using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData item;

    /// <summary>
    /// 상호작용 시에 아이템의 이름을 표시해줍니다.
    /// </summary>
    /// <returns></returns>
    public string GetInteractPrompt()
    {
        return string.Format("pickup {0}", item.displayName);
    }

    /// <summary>
    /// 추후에 인벤토리 구현시 아이템을 획득하는 함수입니다.
    /// </summary>
    public void OnInteract()
    {
        //Inventory.instance.AddItem(item);
        Destroy(gameObject);
    }
}
