using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public string name;
    public int itemQuantity;
    public int enhancementValue;
}

[CreateAssetMenu(fileName = "inventory", menuName = "Iventory", order = 0)]
public class InventorySO : ScriptableObject
{
    public Inventory[] inventory;
}
