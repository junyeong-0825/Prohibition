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
            Debug.LogError("�ʿ��� ������Ʈ�� �Ҵ���� �ʾҽ��ϴ�.");
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
                    // �̹� �����ϴ� �������̸� ������ ����
                    existingItem.Quantity += 1;
                }
                else
                {
                    // �κ��丮�� �������� ������ ���� �߰�
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

                    // �κ��丮�� ���ο� ������ �߰�
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