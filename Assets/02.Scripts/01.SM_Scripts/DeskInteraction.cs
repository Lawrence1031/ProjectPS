using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class DeskInteraction : MonoBehaviour, IInteraction
{
    // Desk에 KeyObj가 존재하고,
    // Player가 ClueKey를 갖고 있으면
    // Player가 Desk와 상호작용하면 ClueKey가 없어지고 KeyObj가 생성되게

    private bool deskHasKey = true;
    private bool playerHasClueKey;

    public ItemData itemData;
    public ItemData ClueKeyObj;
    public ItemData KeyObj;

    public string GetInteractPrompt()
    {
        return string.Format("{0}", itemData.displayName);
    }

    public void OnInteract()
    {
        if (deskHasKey && playerHasClueKey)
        {
            Inventory.instance.AddItem(KeyObj);

            // 인벤토리에서 ClueKeyObj를 제거하는 함수
            //Inventory.instance.Destroy(ClueKeyObj);
        }
        else
        {
            Debug.Log("Clue Key가 필요합니다");
        }
    }

    //private void CheckClueKey()
    //{
    //    // 특정 아이템을 갖고 있는지 확인하는 함수입니다.
    //    Inventory.instance.HasItems(ClueKeyObj);
    //}
}
