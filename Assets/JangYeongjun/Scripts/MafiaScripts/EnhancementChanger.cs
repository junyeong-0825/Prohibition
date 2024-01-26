using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnhancementChanger : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI playerGoldText;
    public BankSO bankSO;
    public ItemSO itemSO;
    public GameObject contents;
    public GameObject enhanceSlotPrefab;
    void Awake()
    {
        GenerateItemSlots();
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
            if (item.Quantity >= 1)
            {
                GameObject slot = Instantiate(enhanceSlotPrefab, contents.transform);
                UpdateSlotUI(slot, item);
            }
        }
    }

    void UpdateSlotUI(GameObject slot, Item item)
    {
        Image imageComponent = slot.transform.GetComponent<Image>();
        if (imageComponent != null && item.sprite != null)
        {
            imageComponent.sprite = item.sprite;
        }

        Button button = slot.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => 
            {
                bankSO.bankData.Gold -= item.SellingPrice * 10;
                item.SellingPrice += item.RiseScale;
            });
        }
    }
}
