using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Store
{
    public string name;
    public int _value;
    public int quantity;
    public Sprite sprite;
}
[CreateAssetMenu(fileName = "Material", menuName = "Store", order = 0)]
public class StoreSO : ScriptableObject
{
    public Store[] store;
}
