using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField] SpriteRenderer imageSprite;
    public bool isUndercover = false;
    public Menu whatServed = Menu.None;
    private Menu result;

    public void IsServed(string menu)
    {
        if (whatServed == Menu.None)
        {
            PlayerInventory existingItem = DataManager.instance.nowPlayer.inventory.Find(invItem => invItem.Name == menu);
            bool success = Enum.TryParse<Menu>(existingItem.Name, out result);
            if (existingItem.Quantity >= 2)
            {
                if (success)
                {
                    whatServed = result;
                    existingItem.Quantity--;
                }
            }
            else if (existingItem.Quantity == 1)
            {
                if (success)
                {
                    whatServed = result;
                    DataManager.instance.nowPlayer.inventory.Remove(existingItem);
                }
            }
            else
            {
                whatServed = Menu.None;
                existingItem = null;
            }   
            UpdateSprite(existingItem);
        }
        
    }

    void UpdateSprite(PlayerInventory existingItem)
    {
        if (existingItem != null) {imageSprite.sprite = Resources.Load<Sprite>(existingItem.spritePath);}
        else { imageSprite.sprite = null; }
    }
    public void OnUnderCovered(InputValue value)
    {
        if (value.isPressed == false)
        {
            return;
        }

        if (!isUndercover)
        {
            Debug.Log("위상상태 돌입");
            isUndercover = true;
        }

        else
        {
            Debug.Log("위장상태 해제");
            isUndercover = false;
        }
    }
}
