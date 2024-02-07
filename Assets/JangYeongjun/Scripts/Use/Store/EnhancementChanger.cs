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
            Debug.LogError("�ʿ��� ������Ʈ�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        // �ʿ��� ��ŭ�� ���Ը� ����
        while (slotList.Count < DataManager.instance.nowPlayer.inventory.Count)
        {
            GameObject slot = Instantiate(enhanceSlotPrefab, contents.transform);
            slotList.Add(slot);
        }

        // ���� ���� �Ǵ� ������Ʈ
        for (int i = 0; i < DataManager.instance.nowPlayer.inventory.Count; i++)
        {
            GameObject slot = slotList[i];
            UpdateSlotUI(slot, DataManager.instance.nowPlayer.inventory[i]);
            slot.SetActive(true);
        }

        // ������� �ʴ� ���� ��Ȱ��ȭ
        for (int i = DataManager.instance.nowPlayer.inventory.Count; i < slotList.Count; i++)
        {
            slotList[i].SetActive(false);
        }
    }

    void UpdateSlotUI(GameObject slot, PlayerInventory inventoryItem)
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

                if (DataManager.instance.nowPlayer.Playerinfo.Gold >= inventoryItem.SellingPrice * 10 && inventoryItem.EnhancementValue < 3)
                {
                    DataManager.instance.nowPlayer.Playerinfo.Gold -= inventoryItem.SellingPrice * 10;
                    ChangePlayerGold();
                    inventoryItem.SellingPrice += inventoryItem.RiseScale;
                    int newEnhancementValue = ++inventoryItem.EnhancementValue;

                    // ���� ��ȭ ������ ���� ������ ã��
                    var sameNamedItem = DataManager.instance.nowPlayer.items
                        .FirstOrDefault(items => items.Name == inventoryItem.Name);

                    if (sameNamedItem != null)
                    {
                        sameNamedItem.SellingPrice += sameNamedItem.RiseScale;
                        sameNamedItem.EnhancementValue += 1;
                    }
                    

                        if (inventoryItem.Quantity == 0)
                        {
                            slot.SetActive(false);
                        }

                    // �κ��丮 UI ������Ʈ
                    GenerateItemSlots();
                }
            });
        }
    }
    public void ChangeDescription(PlayerInventory inventoryItem)
    {
        DescriptionText.text = $"{inventoryItem.Name} {inventoryItem.EnhancementValue} ��, {inventoryItem.Quantity} ��";
    }
    void ChangePlayerGold()
    {
        playerGoldText.text = DataManager.instance.nowPlayer.Playerinfo.Gold.ToString() + " Gold";
    }
}

