using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreChanger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerGoldText;
    public BankSO bankSO;
    public ItemSO itemSO;
    public GameObject contents;
    public GameObject itemSlotPrefab;
    public GameObject _buyButton;
    public GameObject _sellButton;
    bool IsBuying = true;
    void Awake()
    {
        GenerateItemSlots();
        ChangePlayerGold();
    }
    void GenerateItemSlots()
    {
        if (itemSO == null || itemSO.itemList == null || contents == null || itemSlotPrefab == null)
        {
            Debug.LogError("필요한 컴포넌트가 할당되지 않았습니다.");
            return;
        }

        foreach (Item item in itemSO.itemList.items)
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

        // 이미지 컴포넌트를 찾고 업데이트합니다.
        Image spriteImage = slot.transform.Find("ItemImage").GetComponent<Image>();
        if (spriteImage != null && item.sprite != null)
        {
            spriteImage.sprite = item.sprite;
        }

        // 텍스트 컴포넌트를 찾고 업데이트합니다.
        TextMeshProUGUI nameText = slot.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = item.Name;
        }

        TextMeshProUGUI goldText = slot.transform.Find("GoldText").GetComponent<TextMeshProUGUI>();
        if (goldText != null)
        {
            goldText.text = item.PurchasePrice.ToString() + " Gold";
        }

        Image slotImage = slot.GetComponent<Image>();
        Button buyButton = _buyButton.GetComponent<Button>();
        Button sellButton = _sellButton.GetComponent<Button>();
        Button slotButton = slot.GetComponent<Button>();
        if (slotButton != null && buyButton != null && sellButton != null)
        {
            buyButton.onClick.AddListener(() =>
            {
                slotImage.color = Color.yellow;
                IsBuying = true;
            });
            sellButton.onClick.AddListener(() =>
            {
                slotImage.color = Color.blue;
                IsBuying = false;
            });
            slotButton.onClick.AddListener(() =>
            {
                if (IsBuying && bankSO.bankData.Gold > item.PurchasePrice)
                {
                    bankSO.bankData.Gold -= item.PurchasePrice;
                    ChangePlayerGold();
                    item.Quantity += 1;
                }
                else if (!IsBuying && item.Quantity >= 1)
                {
                    bankSO.bankData.Gold += item.SellingPrice;
                    ChangePlayerGold();
                    item.Quantity -= 1;
                }
            });
        }
    }
    void ChangePlayerGold()
    {
        playerGoldText.text = bankSO.bankData.Gold.ToString() + " Gold";
    }
}

