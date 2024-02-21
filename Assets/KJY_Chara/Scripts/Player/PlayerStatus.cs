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
    public Item servingItem;
    private Menu result;

    public void IsServed(string menu)
    {
        if (whatServed != Menu.None)
        {
            PlayerInventory existingItem = DataManager.instance.nowPlayer.inventory.Find(invItem => invItem.Name == menu);

            if (existingItem.Quantity >= 2)
            {
                bool success = Enum.TryParse<Menu>(menu, out result);
                if (success)
                {
                    whatServed = result;
                    existingItem.Quantity--;
                }
            }
            else if (existingItem.Quantity == 1)
            {
                DataManager.instance.nowPlayer.inventory.Remove(existingItem);
            }
            UpdateSprite();
        }
    }

    public void NotServed()
    {
        whatServed = Menu.None;
        servingItem = null;
    }
    void UpdateSprite()
    {
        imageSprite.sprite = Resources.Load<Sprite>(servingItem.spritePath);
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
