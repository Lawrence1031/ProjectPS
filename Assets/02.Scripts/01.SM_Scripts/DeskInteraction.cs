using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class DeskInteraction : MonoBehaviour, IInteraction
{
    private bool deskHasKey = true;
    private bool playerHasClueKey;

    public TextMeshProUGUI UIMessage;

    public ItemData itemData;
    public ItemData ClueKeyObj; // 필요한 키
    public ItemData KeyObj; // 받을 수 있는 키

    public DoorData doorData;

    private void Start()
    {
        UIMessage.gameObject.SetActive(false);
    }

    public string GetInteractPrompt()
    {
        return string.Format("{0}", itemData.displayName);
    }
    public string GetInteratHint()
    {
        return string.Format("{0}", doorData.needKeyName);
    }

    public void OnInteract()
    {
        if (Inventory.instance.HasItems(ClueKeyObj) == true)
        {
            playerHasClueKey = true;
        }

        //Debug.Log("deskHasKey : " + deskHasKey);
        //Debug.Log("playerHasClueKey : " + playerHasClueKey);

        if (deskHasKey && playerHasClueKey)
        {
            Inventory.instance.RemoveItem(ClueKeyObj);
            Inventory.instance.AddItem(KeyObj);
            deskHasKey = false;
        }
        else
        {
            OutputNeedText();
        }
    }

    /// <summary>
    /// 특정 조건에 맞지 않는 경우에 어떤 것이 필요한지 보여주는 텍스트입니다.
    /// </summary>
    private void OutputNeedText()
    {
        UIMessage.text = string.Format("{0}", itemData.description);
        //UIMessage.text = string.Format("{0}이/가 필요합니다.", itemData.displayName);
        UIMessage.gameObject.SetActive(true);

        Invoke("HideUIMessage", 3f);
    }

    private void HideUIMessage()
    {
        UIMessage.gameObject.SetActive(false);
    }
}
