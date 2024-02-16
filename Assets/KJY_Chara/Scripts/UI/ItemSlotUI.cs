using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemSlotUI : MonoBehaviour
{
    //������, �κ��丮, ���� ���� ������
    private PlayerInventory curSlot;
    public Image icon;
    public TextMeshProUGUI quantityText;
    public int index;


    public void Set(PlayerInventory slot)
    {
        curSlot = slot;
        icon.gameObject.SetActive(true);
        icon.sprite = Resources.Load<Sprite>(slot.spritePath);
        quantityText.text = slot.Quantity > 1 ? slot.Quantity.ToString() : string.Empty;
    }

    public void Clear()
    {
        curSlot = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

    
}
