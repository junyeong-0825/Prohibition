using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceManager : MonoBehaviour
{
    public StoreSO storeSO;
    public GameObject contents;
    public GameObject enhanceSlotPrefab;
    public InventorySO inventorySO;
    bool IsAction = false;
    void Awake()
    {
        GenerateItemSlots();
    }

    void GenerateItemSlots()
    {
        if (inventorySO == null || inventorySO.inventory == null || contents == null || enhanceSlotPrefab == null)
        {
            Debug.LogError("필요한 컴포넌트가 할당되지 않았습니다.");
            return;
        }

        foreach (Store store in storeSO.store)
        {
            IsAction = false;
            CheckInventory(store);
        }
    }

    void CheckInventory(Store store)
    {
        foreach (Inventory inventoryItem in inventorySO.inventory)
        {
            if (inventoryItem.name == store.name)
            {
                if (IsAction == true)
                {
                    AddToInventory(inventoryItem);
                }
                else if (IsAction == false && inventoryItem.itemQuantity >= 1)
                {
                    GameObject slot = Instantiate(enhanceSlotPrefab, contents.transform);
                    UpdateSlotUI(slot, store);
                }
            }
        }
    }

    void UpdateSlotUI(GameObject slot, Store store)
    {
        Image imageComponent = slot.transform.GetComponent<Image>();
        if (imageComponent != null && store.sprite != null)
        {
            imageComponent.sprite = store.sprite;
        }

        Button button = slot.GetComponent<Button>();
        if (button != null)
        {
            IsAction = true;
            button.onClick.AddListener(() => CheckInventory(store));
        }
    }
    void AddToInventory(Inventory inventoryItem)
    {
        inventoryItem.enhancementValue += 1;
        return;
    }

}
