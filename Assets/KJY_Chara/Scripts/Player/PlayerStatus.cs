using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

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
    public bool isServed = false;
    public bool isUndercover = false;
    public Menu whatServed = Menu.None;
    private Menu result;

    public void IsServed(string menu)
    {
        isServed = true;
        bool success = Enum.TryParse<Menu>(menu, out result);
        if (success)
        {
            whatServed = result;
        }
        else
        {
            whatServed = Menu.None;
        }
        Item servingItem = DataManager.instance.nowPlayer.items.Find(item => item.Name == whatServed.ToString());

        imageSprite.sprite = Resources.Load<Sprite>(servingItem.spritePath);
    }

    public void NotServed()
    {
        isServed = false;
        whatServed = Menu.None;
    }

    public void OnUnderCovered(InputValue value)
    {
        if (value.isPressed == false)
        {
            return;
        }

        if (!isUndercover)
        {
            Debug.Log("위장상태");
            isUndercover = true;
        }

        else
        {
            Debug.Log("위장해제");
            isUndercover = false;
        }
    }
}
