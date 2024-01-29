using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnhancementChanger : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI playerGoldText;
    [SerializeField] BankSO bankSO;
    public ItemSO itemSO;
    public GameObject contents;
    public GameObject enhanceSlotPrefab;
    [SerializeField] TextMeshProUGUI DescriptionText;
    void Awake()
    {
        GenerateItemSlots();
        ChangePlayerGold();
    }

    void GenerateItemSlots()
    {
        if (contents == null || enhanceSlotPrefab == null)
        {
            Debug.LogError("필요한 컴포넌트가 할당되지 않았습니다.");
            return;
        }

        foreach (Item item in itemSO.itemList.items)
        {
            GameObject slot = Instantiate(enhanceSlotPrefab, contents.transform);
            ChangeDescription(item);
            UpdateSlotUI(slot, item);
        }
    }

    void UpdateSlotUI(GameObject slot, Item item)
    {
        Image imageComponent = slot.GetComponent<Image>();
        if (imageComponent != null && item.sprite != null)
        {
            imageComponent.sprite = item.sprite;
        }
        
        Button button = slot.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => 
            {
                if (bankSO.bankData.Gold >= item.SellingPrice * 10)
                {
                    bankSO.bankData.Gold -= item.SellingPrice * 10;
                    ChangePlayerGold();
                    item.SellingPrice += item.RiseScale;
                }
            });
        }
    }
    public void ChangeDescription(Item item)
    {
        DescriptionText.text = item.Classification;
    }
    void ChangePlayerGold()
    {
        playerGoldText.text = bankSO.bankData.Gold.ToString() + " Gold";
    }
}
