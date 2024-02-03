using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class StoreDataManager : MonoBehaviour
{
    #region URL
    const string MaterialURL = "https://docs.google.com/spreadsheets/d/13vtl_xZLrGFk1j-iw-JTqoMaoQpEFXKu0iNLsITKjyo/export?format=tsv&gid=1341001983&range=A2:G";
    #endregion
    #region Fields
    public static StoreDataManager Instance;
    public Action<float> OnProgressChanged;
    [SerializeField] StoreSO storeSO;
    [SerializeField] InvenSO inventorySO;
    #endregion
    private void Awake()
    {
        #region 싱글톤
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        #endregion
    }
    public void SetStart()
    {
        StartCoroutine(Get());
    }
    IEnumerator Get()
    {
        UnityWebRequest www = UnityWebRequest.Get(MaterialURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            // 데이터 처리 시작
            OnProgressChanged?.Invoke(0.7f);

            SetStoreSO(www.downloadHandler.text);

            // 데이터 처리 완료
            OnProgressChanged?.Invoke(1.0f);
        }
    }

    void SetStoreSO(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowsize = row.Length;
        storeSO.store = new Store[rowsize];
        inventorySO.inven = new Inven[rowsize];
        for (int i = 0; i < rowsize; i++)
        {
            string[] column = row[i].Split('\t');
            if (column.Length >= 7)
            {
                Store stores = new Store();
                Inven inventorys = new Inven();
                stores.name = column[0];
                inventorys.name = column[0];
                stores.classification = column[1];
                int.TryParse(column[2], out stores.maximum);
                stores.descripttion = column[3];
                int.TryParse(column[4], out stores.buyCost);
                float.TryParse(column[5], out stores.sellCost);
                float.TryParse(column[6], out stores.enhancementCost);
                stores.sprite = Resources.Load<Sprite>($"Sprites/{column[0]}");

                inventorySO.inven[i] = inventorys;
                storeSO.store[i] = stores;
            }
        }
        Resources.UnloadUnusedAssets();
    }
}
