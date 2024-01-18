using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Data
{
    public string cardnumber = "6823 6518 5455 4861";
    public string password = "1234";
    public string name = "Rtan";
    public int cash = 100000;
    public int balance = 50000;
    public int debt = 0;
    public int interest = 0;
}
public class DataManager : MonoBehaviour
{
    string path;
    string filename = "userdata";
    public static DataManager instance;
    public Data user = new Data();
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this) 
        {
            Destroy(instance.gameObject);
        }
        path = Application.persistentDataPath + "/";
    }
    public void Start()
    {
        SaveData();
    }
    public void SaveData()
    {
        string data = JsonUtility.ToJson(user);
        File.WriteAllText(path + filename, data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + filename);
        user = JsonUtility.FromJson<Data>(data);
    }
}
