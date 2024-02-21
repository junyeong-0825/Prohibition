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
    public string spritePath;
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
    public string spritePath;
}
[System.Serializable]
public class InventoryWrapper
{
    public List<PlayerInventory> inventory;
}
[System.Serializable]
public class ItemWrapper
{
    public List<Item> items;
}

[System.Serializable]
public class PlayerData
{
    public int Gold = 100;
    public int Debt = 50000;
    public int Day = 1;
}
public class DataManager : MonoBehaviour
{
    internal string playerName;
    public Datas nowPlayer = new Datas();

    string path;

    #region Singleton
    public static DataManager instance;
    private void Awake()
    {
        
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
        
        LoadAllData();
        path = Application.persistentDataPath;
    }
    #endregion

    /*
    #region Save
    public void SetValue()
    {
        WWWForm form = new WWWForm();

        string goldValue = nowPlayer.Playerinfo.Gold.ToString();
        if (goldValue == null) goldValue = "";
        Debug.Log(goldValue);

        string debtValue = nowPlayer.Playerinfo.Debt.ToString();
        if (debtValue == null) debtValue = "";
        Debug.Log(debtValue);

        string tutorialValue = nowPlayer.Playerinfo.DidTutorial.ToString();
        if (tutorialValue == null) tutorialValue = "";
        Debug.Log(tutorialValue);

        string dayValue = nowPlayer.Playerinfo.Day.ToString();
        if (dayValue == null) dayValue = "";
        Debug.Log(dayValue);

        InventoryWrapper invenWrapper = new InventoryWrapper { inventory = nowPlayer.inventory };
        string invenValue = JsonUtility.ToJson(invenWrapper);
        if (invenValue == null) invenValue = "";

        ItemWrapper itemWrapper = new ItemWrapper { items = nowPlayer.items};
        string itemValue = JsonUtility.ToJson(itemWrapper);
        if (itemValue == null) itemValue = "";

        Debug.Log(invenValue);

        form.AddField("order", "setValue");
        form.AddField("gold", goldValue);
        form.AddField("debt", debtValue);
        form.AddField("tutorial", tutorialValue);
        form.AddField("inven", invenValue);
        form.AddField("item", itemValue);
        form.AddField("day", dayValue);

        StartCoroutine(LoginManager.loginInstance.Post(form));
    }
    #endregion
    */

    public void SaveAllData()
    {
        SavePlayerData();
        SaveInventoryData();
        SaveItemData();
    }

    #region SaveDatas
    void SavePlayerData()
    {
        string playerData = JsonUtility.ToJson(nowPlayer.Playerinfo);
        File.WriteAllText(path + "/playerData.json", playerData);
    }
    void SaveInventoryData()
    {
        string InventoryData = JsonUtility.ToJson(nowPlayer.inventory);
        File.WriteAllText(path + "/inventoryData.json", InventoryData);
    }
    void SaveItemData()
    {
        string ItemData = JsonUtility.ToJson(nowPlayer.items);
        File.WriteAllText(path + "/itemData.json", ItemData);
    }
    #endregion

    public void LoadAllData()
    {
        try
        {

            #region ItemData Load
            if (File.Exists(path + "/itemData.json"))
            {
                string ItemData = File.ReadAllText(path + "/itemData.json");
                nowPlayer.items = JsonUtility.FromJson<List<Item>>(ItemData);
            }
            else
            {
                TextAsset itemFile = Resources.Load<TextAsset>("Datas/ItemData");
                if (itemFile == null) throw new Exception("아이템 데이터를 찾을 수 없습니다.");
                Datas itemData = JsonUtility.FromJson<Datas>(itemFile.text);
                nowPlayer.items = itemData.items;
            }
            Debug.Log("아이템 데이터 성공");
            #endregion

            #region PlayerData Load
            if (File.Exists(path + "/playerData.json"))
            {
                string PlayerData = File.ReadAllText(path + "/playerData.json");
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
        }
        catch (Exception e)
        {
            Debug.LogError("데이터를 로드하는데 실패했습니다.: " + e.Message);
        }
    }
}
