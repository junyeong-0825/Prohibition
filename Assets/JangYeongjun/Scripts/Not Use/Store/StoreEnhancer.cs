/*
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreEnhancer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerGoldText;
    [SerializeField] TextMeshProUGUI DescriptionText;
    public GameObject contents;
    public GameObject enhanceSlotPrefab;

    void Start()
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

        foreach (Item item in DataManager.instance.nowPlayer.items)
        {
            if (item.PurchasePrice > 0)
            {
                GameObject slot = Instantiate(enhanceSlotPrefab, contents.transform);
                UpdateSlotUI(slot, item);
            }
        }
    }

    void UpdateSlotUI(GameObject slot, Item item)
    {
        #region Finds & GetComponents
        Image imageComponent = slot.GetComponent<Image>();
        Button slotButton = slot.GetComponent<Button>();
        #endregion

        if (imageComponent != null && item.spritePath != null)
        {
            imageComponent.sprite = Resources.Load<Sprite>(item.spritePath);
        }

        slotButton.onClick.AddListener(() =>
        {
            if (DataManager.instance.nowPlayer.Playerinfo.Gold >= item.PurchasePrice * 15)
            {
                if (item.EnhancementValue < 3)
                {
                    DataManager.instance.nowPlayer.Playerinfo.Gold -= item.SellingPrice * 15;
                    item.SellingPrice += item.RiseScale;
                    item.EnhancementValue++;
                    ChangePlayerGold();
                } 
            }
            
        });
        SlotPointerHandler pointerHandler = slot.AddComponent<SlotPointerHandler>();
        pointerHandler.items = item;
    }
    public void ChangeDescription(Item item)
    {
        DescriptionText.text = $"{item.Name} {item.EnhancementValue} 강, {item.Quantity} 개 강화비용 : {item.SellingPrice*15}";
    }
    void ChangePlayerGold()
    {
        playerGoldText.text = String.Format("{0:N0}", DataManager.instance.nowPlayer.Playerinfo.Gold);
    }
}
*/