using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum Menu
{
    None,   // 0
    Food,   // 1
    Beer,   // 2
    Wine,   // 3
    Whisky  // 4
}


public class PlayerStatus : MonoBehaviour
{
    [SerializeField] internal SpriteRenderer imageSprite;
    [SerializeField] GameObject alcoholPanel;
    [SerializeField] TextMeshPro foodCountText;
    public bool isUndercover = false;
    public Menu whatServed = Menu.None;
    private Menu result;
    private GameObject UnderCoverUI;

    public static PlayerStatus instance;
    private void Awake()
    {
        instance = this;
        UnderCoverUI = GameObject.Find("Emoji");
        UnderCoverUI.SetActive(false);
    }
    private void Start()
    {
        FoodCountUpdate();
    }
    void FoodCountUpdate()
    {
        PlayerInventory foodInven = DataManager.instance.nowPlayer.inventory.Find(item => item.Name == "Food");
        if (foodInven != null) { foodCountText.text = $"{foodInven.Quantity}"; }
        else { foodCountText.text = "0"; }
    }
    public void IsServed(string menu)
    {
        Debug.Log("serving");
        bool success = Enum.TryParse<Menu>(menu, out result);
        if (whatServed == Menu.None)
        {
            Inventory.Instance.ResetUI();
            PlayerInventory existingItem = DataManager.instance.nowPlayer.inventory.Find(invItem => invItem.Name == menu);
            if (existingItem != null)
            {
                if (existingItem.Quantity >= 2)
                {
                    existingItem.Quantity--;
                    ChangeWhatServed(success, result);
                }
                else if (existingItem.Quantity == 1)
                {
                    existingItem.Quantity = 0;
                    DataManager.instance.nowPlayer.inventory.Remove(existingItem);
                    ChangeWhatServed(success, result);
                }
                AlcoholPanelFalse();
            }

            if(menu == "Food") FoodCountUpdate();

            UpdateSprite(existingItem);
        }
        else if (success && whatServed == result) { AlcoholPanelFalse(); ResetStatus(); FoodCountUpdate(); }
    }
    void ChangeWhatServed(bool sucess, Menu result)
    {
        if (sucess)
        {
            whatServed = result;
            Inventory.Instance.UpdateUI();
        }
    }
    void AlcoholPanelFalse()
    {
        if (alcoholPanel.activeSelf)
        {
            alcoholPanel.SetActive(false);
        }
    }

    void UpdateSprite(PlayerInventory existingItem)
    {
        if (existingItem != null) 
        {
            imageSprite.sprite = Resources.Load<Sprite>(existingItem.spritePath);
        }
        else 
        {
            imageSprite.sprite = null; 
        }
    }
    public void OnUnderCovered(InputValue value)
    {
        if (value.isPressed == false)
        {
            return;
        }

        if (!isUndercover)
        {
            UnderCoverUI.SetActive(true);
            isUndercover = true;
        }

        else
        {
            UnderCoverUI.SetActive(false);
            isUndercover = false;
        }
    }
    public void ResetStatus()
    {
        if (whatServed != Menu.None)
        {
            PlayerInventory existingItem = DataManager.instance.nowPlayer.inventory.Find(invItem => invItem.Name == whatServed.ToString());
            if (existingItem != null && existingItem.Quantity >= 1)
            {
                existingItem.Quantity++;
                Inventory.Instance.UpdateUI();
            }
            else if (existingItem == null)
            {
                Item item = DataManager.instance.nowPlayer.items.Find(item => item.Name == whatServed.ToString());
                PlayerInventory newInventoryItem = new PlayerInventory
                {
                    Classification = item.Classification,
                    Name = item.Name,
                    Quantity = 1,
                    PurchasePrice = item.PurchasePrice,
                    SellingPrice = item.SellingPrice,
                    RiseScale = item.RiseScale,
                    spritePath = "Sprites/" + item.Name,
                    EnhancementValue = 0
                };
                DataManager.instance.nowPlayer.inventory.Add(newInventoryItem);
                Inventory.Instance.UpdateUI();
            }
            whatServed = Menu.None;
            imageSprite.sprite = null;
        }
    }
}
