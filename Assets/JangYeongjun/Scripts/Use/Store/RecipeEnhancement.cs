using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class RecipeEnhancement : MonoBehaviour
{
    #region Fields
    [SerializeField] GameObject[] Slots;
    [SerializeField] Button enhancementButton;
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemEnhacementValue;
    [SerializeField] TextMeshProUGUI needMoney;
    [SerializeField] TextMeshProUGUI itemCurruntPrice;
    [SerializeField] TextMeshProUGUI itemEnhancePrice;
    [SerializeField] TextMeshProUGUI playerGold;
    [SerializeField] TextMeshProUGUI errorMessage;
    #endregion

    private void OnEnable()
    {
        UpdatePlayerGold();
        if (DataManager.instance.nowPlayer.items != null)
        {
            foreach (GameObject slot in Slots)
            {
                slot.SetActive(false);
            }
            for (int i = 0; i < DataManager.instance.nowPlayer.items.Count; i++)
            {
                Slots[i].SetActive(true);
                SlotButton(Slots[i], DataManager.instance.nowPlayer.items[i]);
                SlotImage(Slots[i], DataManager.instance.nowPlayer.items[i]);
            }
        }
    }
    void SlotImage(GameObject slot, Item item)
    {
        Image slotImage = slot.GetComponent<Image>();
        slotImage.sprite = Resources.Load<Sprite>(item.spritePath);
    }
    void SlotButton(GameObject slot, Item item)
    {
        Button slotButton = slot.GetComponent<Button>();
        slotButton.onClick.AddListener(() =>
        {
            enhancementButton.onClick.RemoveAllListeners();
            ItemInfoPanelUpdate(item);
            enhancementButton.onClick.AddListener(() => { DoEnhancement(item); });        });
    }
    void DoEnhancement(Item item)
    {
        if (DataManager.instance.nowPlayer.Playerinfo.Gold >= item.SellingPrice * 15)
        {
            if (item.EnhancementValue < 3)
            {
                DataManager.instance.nowPlayer.Playerinfo.Gold -= item.SellingPrice * 15;
                item.SellingPrice += item.RiseScale;
                item.EnhancementValue++;
                enhancementButton.onClick.RemoveAllListeners();
                UpdatePlayerGold();
            }
            else
            {
                errorMessage.text = "Maximum enhancement value has been reached.";
            }
        }
        else
        {
            errorMessage.text = "You don't have enough gold.";
        }
        ItemInfoPanelClear();
    }
    void ItemInfoPanelUpdate(Item item)
    {
        itemImage.color = new Color(1, 1, 1, 1);
        itemImage.sprite = Resources.Load<Sprite>(item.spritePath);
        itemName.text = item.Name;
        itemEnhacementValue.text = $"{item.EnhancementValue}";
        needMoney.text = "-" + String.Format("{0:N0}", item.SellingPrice * 15) + " Gold";
        itemCurruntPrice.text = String.Format("{0:N0}", item.SellingPrice) + " Gold";
        itemEnhancePrice.text = String.Format("{0:N0}", item.SellingPrice + item.RiseScale) + " Gold";
    }
    void ItemInfoPanelClear()
    {
        itemImage.color = new Color(1, 1, 1, 0);
        itemImage.sprite = null;
        itemName.text = "";
        itemEnhacementValue.text = "";
        needMoney.text = "";
        itemCurruntPrice.text = "" ;
        itemEnhancePrice.text = "";
    }
    void UpdatePlayerGold()
    {
        errorMessage.text = "";
        playerGold.text = String.Format("{0:N0}", DataManager.instance.nowPlayer.Playerinfo.Gold) + " Gold";
    }
}
