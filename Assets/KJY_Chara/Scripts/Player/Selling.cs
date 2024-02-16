using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selling : MonoBehaviour
{
    public void Sell(Item item)
    {
        DataManager.instance.nowPlayer.Playerinfo.Gold += item.PurchasePrice;
    }
}
