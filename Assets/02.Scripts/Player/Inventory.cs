using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class ItemSlot
{
    public ItemData item;
    public int quantity;
}

public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform dropPosition;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    //public TextMeshProUGUI selectedItemStatNames;
    //public TextMeshProUGUI selectedItemStatValues;
    public GameObject useButton;
    //public GameObject equipButton;
    //public GameObject unEquipButton;
    public GameObject dropButton;

    //private int curEquipIndex;

    private PlayerController controller;

    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;

    public static Inventory instance;
    void Awake()
    {
        instance = this;
        controller = GetComponent<PlayerController>();
    }
    private void Start()
    {
        inventoryWindow.SetActive(false);
        slots = new ItemSlot[uiSlots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }

        ClearSeletecItemWindow();
    }



    public void OnInventoryButton(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            if (controller.deathWindow.activeInHierarchy == false)
            {
                Toggle();
            }
        }
    }

    public void Toggle()
    {
        if (inventoryWindow.activeInHierarchy)
        {
            inventoryWindow.SetActive(false);
            onCloseInventory?.Invoke();
            controller.ToggleCursor(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
            onOpenInventory?.Invoke();
            controller.ToggleCursor(true);
        }
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    public void AddItem(ItemData item)
    {
        if (item.canStack)
        {
            ItemSlot slotToStackTo = GetItemStack(item);
            if (slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                UpdateUI();
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            UpdateUI();
            return;
        }
        ThrowItem(item);
    }

    void ThrowItem(ItemData item)
    {
        Instantiate(item.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360f));
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
                uiSlots[i].Set(slots[i]);
            else
                uiSlots[i].Clear();
        }
    }

    ItemSlot GetItemStack(ItemData item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item && slots[i].quantity < item.maxStackAmount)
                return slots[i];
        }

        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
                return slots[i];
        }

        return null;
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null)
            return;

        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.item.displayName;
        selectedItemDescription.text = selectedItem.item.description;

        //selectedItemStatNames.text = string.Empty;
        //selectedItemStatValues.text = string.Empty;

    }
    private void ClearSeletecItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;

        //selectedItemStatNames.text = string.Empty;
        //selectedItemStatValues.text = string.Empty;

        useButton.SetActive(false);
        //equipButton.SetActive(false);
        //unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public void OnDropButton()
    {
        ThrowItem(selectedItem.item);
        RemoveSelectedItem();
    }

    private void RemoveSelectedItem()
    {
        selectedItem.quantity--;

        if (selectedItem.quantity <= 0)
        {
            selectedItem.item = null;
            ClearSeletecItemWindow();
        }

        UpdateUI();
    }

    public void RemoveItem(ItemData item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item)
            {
                slots[i].item = null;
            }
        }
    }

    public bool HasItems(ItemData item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item)
            {
                return true;
            }
        }

        return false;
    }
}