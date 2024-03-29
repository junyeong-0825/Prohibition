using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class Inventory : MonoBehaviour
{
    //인벤토리 슬롯의 길이는 TemporaryDataManager.instance.nowPlayer.inventory에 저장됨
    
    public List<ItemSlotUI> uiSlots;

    public GameObject inventoryWindow;
    public TextMeshProUGUI haveGoldText;

    private PlayerInputController controller;


    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;

    public static Inventory Instance;

    private int haveGold;

    private void Awake()
    {
        Instance = this;
        controller = GetComponent<PlayerInputController>();
    }
    private void Start()
    {
        inventoryWindow.SetActive(false);

        for (int i = 0; i < DataManager.instance.nowPlayer.inventory.Count; i++)
        {
            
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }
        UpdateUI();
    }

    public void OnInventory(InputValue context)
    {
            Toggle();
    }

    private void Toggle()
    {
        if (inventoryWindow.activeInHierarchy)
        {
            inventoryWindow.SetActive(false);
            onCloseInventory?.Invoke();
        }
        else
        {
            inventoryWindow.SetActive(true);
            onOpenInventory?.Invoke();
        }
    }
    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    

    public void UpdateUI()
    {
        for(int i = 0; i < DataManager.instance.nowPlayer.inventory.Count; i++)
        {
            if (DataManager.instance.nowPlayer.inventory[i] != null)
            {
                uiSlots[i].Set(DataManager.instance.nowPlayer.inventory[i]);
            }
            else
            {
                uiSlots[i].Clear();
            }
        }
        haveGold = DataManager.instance.nowPlayer.Playerinfo.Gold;
        haveGoldText.text = haveGold.ToString();
    }

    

    PlayerInventory GetEmptySlot()
    {
        for (int i = 0; i < DataManager.instance.nowPlayer.inventory.Count; i++)
        {
            if (DataManager.instance.nowPlayer.inventory[i] == null)
                return DataManager.instance.nowPlayer.inventory[i];
        }
        return null;
    }
}
