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
    public List<MissionData> missions;
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
public class MissionWrapper
{
    public List<MissionData> missions;
}
[System.Serializable]
public class PlayerData
{
    public int Gold = 100;
    public int Debt = 50000;
    public int Day = 1;
    public bool IsDay = true;
    public bool IsTutorialed = false;
}
[System.Serializable]
public class MissionData
{
    public string Name;
    public string Script;
    public string Description;
    public string Reward;
    public bool DidMission;
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
    }
    #endregion

    #region SaveDatas
    public void SaveAllData()
    {
        SavePlayerData();
        SaveInventoryData();
        SaveItemData();
        SaveMissionData();
    }

    void SavePlayerData()
    {
        string playerData = JsonUtility.ToJson(nowPlayer.Playerinfo, true);
        string encryptedPlayerData = EncryptionUtility.EncryptString(playerData);
        File.WriteAllText(path + "/playerData.json", encryptedPlayerData);
    }
    void SaveInventoryData()
    {
        InventoryWrapper invenWrapper = new InventoryWrapper { inventory = nowPlayer.inventory };
        string InventoryData = JsonUtility.ToJson(invenWrapper, true);
        string encryptedInventoryData = EncryptionUtility.EncryptString(InventoryData);
        File.WriteAllText(path + "/inventoryData.json", encryptedInventoryData);
    }
    void SaveItemData()
    {
        ItemWrapper itemWrapper = new ItemWrapper { items = nowPlayer.items };
        string ItemData = JsonUtility.ToJson(itemWrapper, true);
        string encryptedItemData = EncryptionUtility.EncryptString(ItemData);
        File.WriteAllText(path + "/itemData.json", encryptedItemData);
    }
    void SaveMissionData()
    {
        MissionWrapper missionWrapper = new MissionWrapper { missions = nowPlayer.missions };
        string missionData = JsonUtility.ToJson(missionWrapper, true);
        string encryptedMissionData = EncryptionUtility.EncryptString(missionData);
        File.WriteAllText(path + "/missionData.json", encryptedMissionData);
    }
    #endregion

    #region LoadDatas
    public void LoadAllData()
    {
        try
        {
            bool result = CheckDatas();
            #region ItemData Load
            if(result)
            {
                string ItemData = File.ReadAllText(path + "/itemData.json");
                string InventoryData = File.ReadAllText(path + "/inventoryData.json");
                string PlayerData = File.ReadAllText(path + "/playerData.json");
                string MissionData = File.ReadAllText(path + "/missionData.json");

                string decryptedItemData = EncryptionUtility.DecryptString(ItemData);
                string decryptedInventoryData = EncryptionUtility.DecryptString(InventoryData);
                string decryptedPlayerData = EncryptionUtility.DecryptString(PlayerData);
                string decryptedMissionData = EncryptionUtility.DecryptString(MissionData);

                ItemWrapper itemWrapper = JsonUtility.FromJson<ItemWrapper>(decryptedItemData);
                InventoryWrapper inventoryWrapper = JsonUtility.FromJson<InventoryWrapper>(decryptedInventoryData);
                MissionWrapper missionWrapper = JsonUtility.FromJson<MissionWrapper>(decryptedMissionData);

                nowPlayer.items = itemWrapper.items;
                nowPlayer.inventory = inventoryWrapper.inventory;
                nowPlayer.missions = missionWrapper.missions;
                nowPlayer.Playerinfo = JsonUtility.FromJson<PlayerData>(decryptedPlayerData);
            }
            else 
            {
                DeleteAllData();

                TextAsset itemFile = Resources.Load<TextAsset>("Datas/ItemData");
                TextAsset inventoryFile = Resources.Load<TextAsset>("Datas/InventoryData");
                TextAsset missionFile = Resources.Load<TextAsset>("Datas/MissionData");

                if (itemFile == null) throw new Exception("아이템 데이터를 찾을 수 없습니다.");
                if (inventoryFile == null) throw new Exception("인벤토리 데이터를 찾을 수 없습니다.");
                if (missionFile == null) throw new Exception("미션 데이터를 찾을 수 없습니다.");

                Datas itemData = JsonUtility.FromJson<Datas>(itemFile.text);
                Datas inventoryData = JsonUtility.FromJson<Datas>(inventoryFile.text);
                Datas MissionData = JsonUtility.FromJson<Datas>(missionFile.text);

                nowPlayer.items = itemData.items;
                nowPlayer.inventory = inventoryData.inventory;
                nowPlayer.missions = MissionData.missions;
                nowPlayer.Playerinfo = new PlayerData();
            }
            #endregion
        }
        catch (Exception e)
        {
            Debug.LogError("데이터를 로드하는데 실패했습니다.: " + e.Message);
        }
    }
    #endregion

    #region DeleteDatas
    public void DeleteAllData()
    {
        File.Delete(path + "/playerData.json");
        File.Delete(path + "/inventoryData.json");
        File.Delete(path + "/itemData.json");
        File.Delete(path + "/missionData.json");
    }
    #endregion

    #region CheckDatas
    public bool CheckDatas()
    {
        if (File.Exists(path + "/playerData.json") && File.Exists(path + "/itemData.json") && File.Exists(path + "/inventoryData.json") && File.Exists(path + "/missionData.json")) { return true; }
        else { return false; }
    }
    #endregion

}
