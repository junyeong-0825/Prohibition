using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public int coin = 10000;
}
public class TemporaryDataManager : MonoBehaviour
{
    public static TemporaryDataManager instance;

    public PlayerData nowPlayer = new PlayerData();

    public string path;

    private void Awake()
    {
        #region ΩÃ±€≈Ê
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        path = Application.persistentDataPath + "/saves";
    }
    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path, data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path);
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }
}
