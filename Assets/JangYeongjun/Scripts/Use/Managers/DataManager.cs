using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[System.Serializable]
public class Datas
{
    public PlayerData Playerinfo;
    public List<Item> items;
    public List<PlayerInventory> inventory;
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
    public int EnhancementValue;
    public Sprite sprite;
}

[System.Serializable]
public class PlayerInventory
{
    public string Classification;
    public string Name;
    public int Quantity;
    public int PurchasePrice;
    public int SellingPrice;
    public int RiseScale;
    public int EnhancementValue;
    public Sprite sprite;
}

[System.Serializable]
public class PlayerData
{
    public int Gold = 100;
    public int Debt = 50000;
}
public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public Datas nowPlayer = new Datas();

    string path;

    private void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        if (transform.parent != null)
        {
            DontDestroyOnLoad(transform.parent.root.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        
        //LoadAllData();
        #endregion

        path = Application.persistentDataPath;
    }
    public void SaveAllData()
    {
        SavePlayerData();
        SaveInventoryData();
    }
    public void SavePlayerData()
    {
        string playerData = JsonUtility.ToJson(nowPlayer.Playerinfo);
        File.WriteAllText(path + "/playerData.json", playerData);
    }
    public void SaveInventoryData()
    {
        string InventoryData = JsonUtility.ToJson(nowPlayer.inventory);
        File.WriteAllText(path + "/inventoryData.json", InventoryData);
    }

    public void LoadAllData()
    {
        try
        {
            
            #region ItemData Load
            TextAsset itemFile = Resources.Load<TextAsset>("Datas/ItemData");
            if (itemFile == null) throw new Exception("아이템 데이터를 찾을 수 없습니다.");
            Datas itemData = JsonUtility.FromJson<Datas>(itemFile.text);
            nowPlayer.items = itemData.items;

            foreach (Item item in nowPlayer.items)
            {
                item.sprite = Resources.Load<Sprite>("Sprites/" + item.Name);
            }
            #endregion
            /*
            #region PlayerData Load
            if (File.Exists(path + "/playerData.json"))
            {
                string PlayerData = File.ReadAllText(path + "playerData.json");
                nowPlayer.Playerinfo = JsonUtility.FromJson<PlayerData>(PlayerData);
            }
            else
            {
                nowPlayer.Playerinfo = new PlayerData();
            }
            #endregion
            
            #region InventoryData Load
            if (File.Exists(path + "/inventoryData.json"))
            {
                string InventoryData = File.ReadAllText(path + "/inventoryData.json");
                nowPlayer.inventory = JsonUtility.FromJson<List<PlayerInventory>>(InventoryData);
            }
            else
            {
                nowPlayer.inventory = new List<PlayerInventory>();
            }
            #endregion
            */
        }
        catch (Exception e)
        {
            Debug.LogError("데이터를 로드하는데 실패했습니다.: " + e.Message);
        }
    }
}
