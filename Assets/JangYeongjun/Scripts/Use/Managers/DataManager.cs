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
        path = Application.persistentDataPath;
        LoadAllData();
        SaveAllData();
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
        string playerData = JsonUtility.ToJson(nowPlayer.Playerinfo, true);
        File.WriteAllText(path + "/playerData.json", playerData);
    }
    void SaveInventoryData()
    {
        InventoryWrapper invenWrapper = new InventoryWrapper { inventory = nowPlayer.inventory };
        string InventoryData = JsonUtility.ToJson(invenWrapper, true);
        File.WriteAllText(path + "/inventoryData.json", InventoryData);
    }
    void SaveItemData()
    {
        ItemWrapper itemWrapper = new ItemWrapper { items = nowPlayer.items };
        string ItemData = JsonUtility.ToJson(itemWrapper, true);
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
                Debug.Log("Local Item Data ");
                string ItemData = File.ReadAllText(path + "/itemData.json");
                ItemWrapper itemWrapper = JsonUtility.FromJson<ItemWrapper>(ItemData);
                nowPlayer.items = itemWrapper.items;
            }
            else
            {
                Debug.Log("Base Item Data ");
                TextAsset itemFile = Resources.Load<TextAsset>("Datas/ItemData");
                if (itemFile == null) throw new Exception("������ �����͸� ã�� �� �����ϴ�.");
                Datas itemData = JsonUtility.FromJson<Datas>(itemFile.text);
                nowPlayer.items = itemData.items;
            }
            Debug.Log("������ ������ ����");
            #endregion

            #region PlayerData Load
            if (File.Exists(path + "/playerData.json"))
            {
                Debug.Log("Local Player Data ");
                string PlayerData = File.ReadAllText(path + "/playerData.json");
                nowPlayer.Playerinfo = JsonUtility.FromJson<PlayerData>(PlayerData);
            }
            else
            {
                Debug.Log("Base Player Data ");
                nowPlayer.Playerinfo = new PlayerData();
            }
            #endregion
            
            #region InventoryData Load
            if (File.Exists(path + "/inventoryData.json"))
            {
                Debug.Log("Local Inventory Data ");
                string InventoryData = File.ReadAllText(path + "/inventoryData.json");
                InventoryWrapper inventoryWrapper = JsonUtility.FromJson<InventoryWrapper>(InventoryData);
                nowPlayer.inventory = inventoryWrapper.inventory;
            }
            else
            {
                Debug.Log("Base Inventory Data ");
                TextAsset inventoryFile = Resources.Load<TextAsset>("Datas/InventoryData");
                if (inventoryFile == null) throw new Exception("�κ��丮 �����͸� ã�� �� �����ϴ�.");
                Datas inventoryData = JsonUtility.FromJson<Datas>(inventoryFile.text);
                nowPlayer.inventory = inventoryData.inventory;
            }
            #endregion
        }
        catch (Exception e)
        {
            Debug.LogError("�����͸� �ε��ϴµ� �����߽��ϴ�.: " + e.Message);
        }
    }
}
