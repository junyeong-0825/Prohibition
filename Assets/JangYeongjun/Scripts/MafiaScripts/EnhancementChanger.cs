using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnhancementChanger : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI playerGoldText;
    public GameObject contents;
    public GameObject enhanceSlotPrefab;
    [SerializeField] TextMeshProUGUI DescriptionText;
    private List<GameObject> slotList = new List<GameObject>();
    void OnEnable()
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

        // 필요한 만큼의 슬롯만 보장
        while (slotList.Count < TemporaryDataManager.instance.nowPlayer.inventory.Count)
        {
            GameObject slot = Instantiate(enhanceSlotPrefab, contents.transform);
            slotList.Add(slot);
        }

        // 슬롯 재사용 또는 업데이트
        for (int i = 0; i < TemporaryDataManager.instance.nowPlayer.inventory.Count; i++)
        {
            GameObject slot = slotList[i];
            UpdateSlotUI(slot, TemporaryDataManager.instance.nowPlayer.inventory[i]);
            slot.SetActive(true);
        }

        // 사용하지 않는 슬롯 비활성화
        for (int i = TemporaryDataManager.instance.nowPlayer.inventory.Count; i < slotList.Count; i++)
        {
            slotList[i].SetActive(false);
        }
    }

    void UpdateSlotUI(GameObject slot, TemporaryInventory inventoryItem)
    {
        Image imageComponent = slot.GetComponent<Image>();
        Button button = slot.GetComponent<Button>();

        if (imageComponent != null && inventoryItem.sprite != null)
        {
            imageComponent.sprite = inventoryItem.sprite;
        }

        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {

                if (TemporaryDataManager.instance.nowPlayer.Playerinfo.Gold >= inventoryItem.SellingPrice * 10 && inventoryItem.EnhancementValue < 3)
                {
                    TemporaryDataManager.instance.nowPlayer.Playerinfo.Gold -= inventoryItem.SellingPrice * 10;
                    ChangePlayerGold();
                    inventoryItem.SellingPrice += inventoryItem.RiseScale;
                    int newEnhancementValue = ++inventoryItem.EnhancementValue;

                    // 동일 강화 레벨을 가진 아이템 찾기
                    var sameEnhancedItem = TemporaryDataManager.instance.nowPlayer.inventory
                        .FirstOrDefault(inv => inv.Name == inventoryItem.Name && inv.EnhancementValue == newEnhancementValue);

                    if (sameEnhancedItem != null)
                    {
                        // 이미 존재하면 수량 증가
                        sameEnhancedItem.Quantity += 1;
                        // 원래 아이템 수량 감소
                        inventoryItem.Quantity -= 1;
                    }
                    else
                    {
                        // 없으면 새로운 아이템 생성 및 추가
                        TemporaryInventory newInventoryItem = new TemporaryInventory
                        {
                            Classification = inventoryItem.Classification,
                            Name = inventoryItem.Name,
                            Quantity = 1,
                            PurchasePrice = inventoryItem.PurchasePrice,
                            SellingPrice = inventoryItem.SellingPrice,
                            RiseScale = inventoryItem.RiseScale,
                            EnhancementValue = newEnhancementValue,
                            sprite = inventoryItem.sprite
                        };
                        TemporaryDataManager.instance.nowPlayer.inventory.Add(newInventoryItem);
                        // 원래 아이템 수량 감소
                        inventoryItem.Quantity -= 1;
                    }
                    
                    if(inventoryItem.Quantity == 0)
                    {
                        slot.SetActive(false);
                    }
                    // 인벤토리 UI 업데이트
                    GenerateItemSlots();
                }
            });
        }
        SlotPointerHandler pointerHandler = slot.AddComponent<SlotPointerHandler>();
        pointerHandler.inventoryItem = inventoryItem;
    }
    public void ChangeDescription(TemporaryInventory inventoryItem)
    {
        DescriptionText.text = $"{inventoryItem.Name} {inventoryItem.EnhancementValue} 강, {inventoryItem.Quantity} 개";
    }
    void ChangePlayerGold()
    {
        playerGoldText.text = TemporaryDataManager.instance.nowPlayer.Playerinfo.Gold.ToString() + " Gold";
    }
}

