using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ItemSlot
{
    public Item items;
}
public class Inventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public ItemSlotUI[] uiSlots;

    public GameObject inventoryWindow;

    private PlayerInputController controller;

    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;

    public static Inventory Instance;
    private void Awake()
    {
        Instance = this;
        controller = GetComponent<PlayerInputController>();
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
    }

    public void OnInventoryButton(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            Toggle();
        }
    }

    private void Toggle()
    {
        if (inventoryWindow.activeInHierarchy)
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }
    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    public void AddItem(Item item)
    {
        ItemSlot slotToStack = GetItemStack(item);
        if (slotToStack != null)
        {
            slotToStack.items.Quantity++;
            UpdateUI();
            return;
        }

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.items = item;
            emptySlot.items.Quantity = 1;
            UpdateUI();
            return;
        }
    }

    void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].items != null)
            {
                uiSlots[i].Set(slots[i]);
            }
            else
            {
                uiSlots[i].Clear();
            }
        }
    }

    ItemSlot GetItemStack(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].items == item && slots[i].items.Quantity < 100)
                return slots[i];
        }
        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].items == null)
                return slots[i];
        }
        return null;
    }
}
