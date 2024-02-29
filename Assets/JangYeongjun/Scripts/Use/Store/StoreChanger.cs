using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemSlot
{
    public int inputValue = 1;
    public TMP_InputField itemCountInput;
    // 필요한 경우 여기에 더 많은 정보 추가

    public StoreItemSlot(TMP_InputField inputField)
    {
        this.itemCountInput = inputField;
        // 초기화 코드 추가 가능
    }
    public void LimitInputValue(string input)
    {
        int value;
        if (int.TryParse(input, out value))
        {
            if (value < 1)
            {
                itemCountInput.text = "1";
                inputValue = 1;
            }
            else
            {
                inputValue = value;
            }
        }
        else if (string.IsNullOrEmpty(input))
        {
            itemCountInput.text = "1";
            inputValue = 1;
        }
    }
    // inputValue를 증가시키고 입력 필드를 업데이트하는 메서드
    public void UpInputValue()
    {
        inputValue++;
        itemCountInput.text = inputValue.ToString();
    }

    // inputValue를 감소시키고 입력 필드를 업데이트하는 메서드
    public void DownInputValue()
    {
        if (inputValue > 1)
        {
            inputValue--;
            itemCountInput.text = inputValue.ToString();
        }
    }
}
public class StoreChanger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerGoldText;
    GameObject store;
    public GameObject contents;
    public GameObject itemSlotPrefab;
    

    void Start()
    {
        GenerateItemSlots();
        ChangePlayerGold();
    }

    private void OnEnable()
    {
        ChangePlayerGold();
    }

    void GenerateItemSlots()
    {
        if (contents == null || itemSlotPrefab == null)
        {
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
        Image spriteImage = slot.transform.Find("ItemImage").GetComponent<Image>();
        if (spriteImage != null && item.spritePath != null) { spriteImage.sprite = Resources.Load<Sprite>(item.spritePath); }
        
        TextMeshProUGUI nameText = slot.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        if (nameText != null) { nameText.text = item.Name; }

        TextMeshProUGUI goldText = slot.transform.Find("GoldText").GetComponent<TextMeshProUGUI>();
        if(goldText != null) { goldText.text = item.PurchasePrice.ToString() + " Gold"; }
        
        TMP_InputField itemCountInput = slot.transform.Find("ItemCountInput").GetComponent<TMP_InputField>();
        StoreItemSlot itemSlot = new StoreItemSlot(itemCountInput);
        if (itemCountInput != null)
        {
            itemCountInput.onValueChanged.AddListener(itemSlot.LimitInputValue);
        }

        Button slotButton = slot.GetComponent<Button>();
        if (slotButton != null)
        {
            slotButton.onClick.AddListener(() =>
        {
            ResultStoreButton(item, itemSlot.inputValue);
        });
        }

        Button upButton = slot.transform.Find("UpButton").GetComponent<Button>();
        if (upButton != null) { upButton.onClick.AddListener(() => itemSlot.UpInputValue()); }

        Button downButton = slot.transform.Find("DownButton").GetComponent<Button>();
        if (downButton != null) { downButton.onClick.AddListener(() => itemSlot.DownInputValue()); }
    }

    void ResultStoreButton(Item item, int inputValue)
    {
        if (DataManager.instance.nowPlayer.Playerinfo.Gold >= item.PurchasePrice * inputValue)
        {
            DataManager.instance.nowPlayer.Playerinfo.Gold -= item.PurchasePrice * inputValue;
            ChangePlayerGold();
            AddInventory(item, inputValue);
        }
    }
    void AddInventory(Item item, int inputValue)
    {
        PlayerInventory existingItem = DataManager.instance.nowPlayer.inventory.Find(invItem => invItem.Name == item.Name);
        if (existingItem != null)
        {
            // 이미 존재하는 아이템이면 수량만 증가
            existingItem.Quantity += inputValue;
        }
        else
        {
            // 인벤토리에 아이템이 없으면 새로 추가
            PlayerInventory newInventoryItem = new PlayerInventory
            {
                Classification = item.Classification,
                Name = item.Name,
                Quantity = inputValue,
                PurchasePrice = item.PurchasePrice,
                SellingPrice = item.SellingPrice,
                RiseScale = item.RiseScale,
                spritePath = "Sprites/" + item.Name,
                EnhancementValue = 0
            };

            // 인벤토리에 새로운 아이템 추가
            DataManager.instance.nowPlayer.inventory.Add(newInventoryItem);
        }
        GameEvents.NotifyInventoryChanged();
        Inventory.Instance.UpdateUI();
    }

    void ChangePlayerGold()
    {
        playerGoldText.text = String.Format("{0:N0}", DataManager.instance.nowPlayer.Playerinfo.Gold);
    }
}
