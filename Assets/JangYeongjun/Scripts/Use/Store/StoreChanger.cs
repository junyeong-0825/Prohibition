using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreChanger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerGoldText;
    public GameObject contents;
    public GameObject itemSlotPrefab;

    void Start()
    {
        GenerateItemSlots();
        ChangePlayerGold();
    }

    void GenerateItemSlots()
    {
        if (contents == null || itemSlotPrefab == null)
        {
            Debug.LogError("필요한 컴포넌트가 할당되지 않았습니다.");
            return;
        }

        foreach (Item item in DataManager.instance.nowPlayer.items)
        {
            if (item.PurchasePrice > 0)
            {
                GameObject slot = Instantiate(itemSlotPrefab, contents.transform);
                UpdateSlotUI(slot, item);
            }
        }
    }

    void UpdateSlotUI(GameObject slot, Item item)
    {
        #region Finds & GetComponents
        Image spriteImage = slot.transform.Find("ItemImage").GetComponent<Image>();
        TextMeshProUGUI nameText = slot.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI goldText = slot.transform.Find("GoldText").GetComponent<TextMeshProUGUI>();
        Button slotButton = slot.GetComponent<Button>();
        #endregion

        if (spriteImage != null && item.spritePath != null)
        {
            spriteImage.sprite = Resources.Load<Sprite>(item.spritePath);
        }

        if (nameText != null)
        {
            nameText.text = item.Name;
        }

        if(goldText != null)
        {
            goldText.text = item.PurchasePrice.ToString() + " Gold";
        }

        slotButton.onClick.AddListener(() =>
        {
            if (DataManager.instance.nowPlayer.Playerinfo.Gold >= item.PurchasePrice)
            {
                DataManager.instance.nowPlayer.Playerinfo.Gold -= item.PurchasePrice;
                ChangePlayerGold();

                PlayerInventory existingItem = DataManager.instance.nowPlayer.inventory.Find(invItem => invItem.Name == item.Name);

                if (existingItem != null && existingItem.EnhancementValue < 1)
                {
                    // 이미 존재하는 아이템이면 수량만 증가
                    existingItem.Quantity += 1;
                }
                else
                {
                    // 인벤토리에 아이템이 없으면 새로 추가
                    PlayerInventory newInventoryItem = new PlayerInventory
                    {
                        Classification = item.Classification,
                        Name = item.Name,
                        Quantity = 1,
                        PurchasePrice = item.PurchasePrice,
                        SellingPrice = item.SellingPrice,
                        RiseScale = item.RiseScale,
                        spritePath = "Sprites/" + item.Name,
                        EnhancementValue = 0
                    };

                    // 인벤토리에 새로운 아이템 추가
                    DataManager.instance.nowPlayer.inventory.Add(newInventoryItem);
                }
            }
            Inventory.Instance.UpdateUI();
        });
    }
    void ChangePlayerGold()
    {
        playerGoldText.text = String.Format("{0:N0}", DataManager.instance.nowPlayer.Playerinfo.Gold);
    }
}