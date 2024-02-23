using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemSlotUI : MonoBehaviour
{
    //아이템, 인벤토리, 빚에 대한 데이터
    private PlayerInventory curSlot;
    public Image icon;
    public TextMeshProUGUI quantityText;
    public int index;


    public void Set(PlayerInventory slot)
    {
        curSlot = slot;
        icon.sprite = Resources.Load<Sprite>(slot.spritePath);
        quantityText.text = slot.Quantity > 1 ? slot.Quantity.ToString() : string.Empty;
    }

    public void Clear()
    {
        curSlot = null;
        icon.sprite = null;
        quantityText.text = string.Empty;
    }

    
}
