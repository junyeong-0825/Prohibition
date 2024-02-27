using System;
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
    public bool isUndercover = false;
    public Menu whatServed = Menu.None;
    private Menu result;
    private GameObject UnderCoverUI;
    [SerializeField] GameObject alcoholPanel;

    public static PlayerStatus instance;
    private void Awake()
    {
        instance = this;
        UnderCoverUI = GameObject.Find("Emoji");
        UnderCoverUI.SetActive(false);
    }
    public void IsServed(string menu)
    {
        if (whatServed == Menu.None)
        {
            Inventory.Instance.ResetUI();
            PlayerInventory existingItem = DataManager.instance.nowPlayer.inventory.Find(invItem => invItem.Name == menu);
            if (existingItem != null)
            {
                bool success = Enum.TryParse<Menu>(existingItem.Name, out result);
                if (existingItem.Quantity >= 2)
                {
                    if (success)
                    {
                        whatServed = result;
                        existingItem.Quantity--;
                        Inventory.Instance.UpdateUI();
                    }
                }
                else if (existingItem.Quantity == 1)
                {
                    if (success)
                    {
                        whatServed = result;
                        existingItem.Quantity = 0;
                        DataManager.instance.nowPlayer.inventory.Remove(existingItem);
                        Inventory.Instance.UpdateUI();
                    }
                }

                if(alcoholPanel.activeSelf)
                {
                    alcoholPanel.SetActive(false);
                }
            }
            UpdateSprite(existingItem);
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
}
