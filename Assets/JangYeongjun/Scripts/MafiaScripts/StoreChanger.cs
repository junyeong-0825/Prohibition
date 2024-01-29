using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreChanger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerGoldText;
    [SerializeField] BankSO bankSO;
    [SerializeField] ItemSO itemSO;
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
                slot.SetActive(false);
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
        Image slotImage = slot.GetComponent<Image>();
        Button buyButton = _buyButton.GetComponent<Button>();
        Button sellButton = _sellButton.GetComponent<Button>();
        Button slotButton = slot.GetComponent<Button>();
        #endregion

        if (spriteImage != null && item.sprite != null)
        {
            spriteImage.sprite = item.sprite;
        }

        if (nameText != null)
        {
            nameText.text = item.Name;
        }
        if (slotButton != null && buyButton != null && sellButton != null)
        {
            buyButton.onClick.AddListener(() =>
            {
                slot.SetActive(true);
                slotImage.color = Color.yellow;
                goldText.text = item.PurchasePrice.ToString() + " Gold";
                IsBuying = true;
            });
            sellButton.onClick.AddListener(() =>
            {
                slot.SetActive(true);
                slotImage.color = Color.blue;
                goldText.text = item.SellingPrice.ToString() + " Gold";
                IsBuying = false;
            });
            slotButton.onClick.AddListener(() =>
            {
                if (IsBuying)
                {
                    if (bankSO.bankData.Gold >= item.PurchasePrice)
                    {
                        bankSO.bankData.Gold -= item.PurchasePrice;
                        ChangePlayerGold();
                        item.Quantity += 1;
                    }
                }
                else
                {
                    if (item.Quantity >= 1)
                    {
                        bankSO.bankData.Gold += item.SellingPrice;
                        ChangePlayerGold();
                        item.Quantity -= 1;
                    }
                }
            });
        }
    }
    void ChangePlayerGold()
    {
        playerGoldText.text = bankSO.bankData.Gold.ToString() + " Gold";
    }
}

