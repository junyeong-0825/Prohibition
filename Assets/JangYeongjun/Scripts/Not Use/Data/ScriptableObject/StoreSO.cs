using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Store
{
    public string name;
    public string classification;
    public int maximum;
    public string descripttion;
    public int buyCost;
    public float sellCost;
    public float enhancementCost;
    public Sprite sprite;
}
[CreateAssetMenu(fileName = "stroe", menuName = "Store", order = 0)]
public class StoreSO : ScriptableObject
{
    public Store[] store;
}
