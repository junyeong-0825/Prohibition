using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    //아이템, 인벤토리, 빚에 대한 데이터
    private ItemSlot curSlot;
    public Image icon;
    public TextMeshProUGUI quantityText;
    public int index;


    public void Set(ItemSlot slot)
    {
        curSlot = slot;
        icon.gameObject.SetActive(true);
        icon.sprite = slot.items.sprite;
        quantityText.text = slot.items.Quantity > 1 ? slot.items.Quantity.ToString() : string.Empty;
    }

    public void Clear()
    {
        curSlot = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

    
}
