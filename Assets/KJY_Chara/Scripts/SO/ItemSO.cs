using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemList", menuName = "Custom/Item List", order = 1)]
public class ItemSO : ScriptableObject
{
    public ItemList itemList;
}
[System.Serializable]
public class ItemList
{
    public List<Item> items;
}
[System.Serializable]
public class Item
{
    public string Classification;
    public string Name;
    public int Quantity;
    public int PurchasePrice;
    public int SellingPrice;
    public int RiseScale;
    public Sprite sprite;
}
