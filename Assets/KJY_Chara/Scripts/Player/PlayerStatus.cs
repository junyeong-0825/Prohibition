using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] Image servingImage;
    public bool isServed = false;
    public bool isUndercover = false;
    public Menu whatServed = Menu.None;


    public void IsServed(int index)
    {
        isServed = true;
        whatServed = (Menu)index;
        Item servingItem = DataManager.instance.nowPlayer.items.Find(item => item.Name == whatServed.ToString());

        servingImage.sprite = servingItem.sprite;

    }

    public void NotServed()
    {
        isServed = false;
        whatServed = Menu.None;
    }
}
