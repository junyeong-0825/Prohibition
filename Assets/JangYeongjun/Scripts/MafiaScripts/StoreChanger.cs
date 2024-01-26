using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreChanger : MonoBehaviour
{
    public ItemSO itemSO;
    public ItemList itemList;
    public GameObject contents;
    public GameObject itemSlotPrefab;
    public InventorySO inventorySO;
    void Awake()
    {
        GenerateItemSlots();
    }

    void GenerateItemSlots()
    {
        if (itemSO == null || itemSO.itemList == null || contents == null || itemSlotPrefab == null)
        {
            Debug.LogError("필요한 컴포넌트가 할당되지 않았습니다.");
            return;
        }

        foreach (Item item in itemList.items)
        {
            GameObject slot = Instantiate(itemSlotPrefab, contents.transform);
            UpdateSlotUI(slot, item);
        }
    }

    void UpdateSlotUI(GameObject slot, Item item)
    {
        /*
        // 이미지 컴포넌트를 찾고 업데이트합니다.
        Image imageComponent = slot.transform.Find("ItemImage").GetComponent<Image>();
        if (imageComponent != null && item.sprite != null)
        {
            imageComponent.sprite = item.sprite;
        }
        */

        // 텍스트 컴포넌트를 찾고 업데이트합니다.
        TextMeshProUGUI textComponent = slot.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        if (textComponent != null)
        {
            textComponent.text = item.Name;
        }

        TextMeshProUGUI textComponent2 = slot.transform.Find("GoldText").GetComponent<TextMeshProUGUI>();
        if (textComponent2 != null)
        {
            textComponent2.text = item.PurchasePrice.ToString() + " Gold";
        }

        Button button = slot.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => AddToInventory(item));
        }
    }
    

    void AddToInventory(Item item)
    {
        foreach (Inventory inventoryItem in inventorySO.inventory)
        {
            if (inventoryItem.name == item.Name)
            {
                inventoryItem.itemQuantity += 1;
                return;
            }
        }
    }
}
    /*
    public GameObject[] prefeb;
    public List<GameObject>[] pools;
    private void Awake()
    {
        pools = new List<GameObject>[prefeb.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }
    private void OnEnable()
    {
        if (storeSO != null) 
        {
            int _index = UnityEngine.Random.Range(0, pools.Length);
            SpawnFromPool(_index);
        }
    }
    public GameObject SpawnFromPool(int index)
    {
        GameObject select = null;
        foreach (GameObject pool in pools[index])
        {
            if (!pool.activeSelf)
            {
                select = pool;
                select.SetActive(true);
                StartCoroutine(DeactivateTrapAfterDelay(select, 2f));
                break;
            }
        }
        if (!select)
        {
            select = Instantiate(prefeb[index], transform);
            pools[index].Add(select);
            StartCoroutine(DeactivateTrapAfterDelay(select, 2f));
        }

        return select;
    }
    IEnumerator DeactivateTrapAfterDelay(GameObject select, float delay)
    {
        yield return new WaitForSeconds(delay);

        select.SetActive(false);
    }
}
*/   
