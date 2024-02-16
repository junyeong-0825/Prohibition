/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ItemDataManager : MonoBehaviour
{
    #region URL
    const string MaterialURL = "https://docs.google.com/spreadsheets/d/13vtl_xZLrGFk1j-iw-JTqoMaoQpEFXKu0iNLsITKjyo/export?format=tsv&gid=1341001983&range=A2:G";
    #endregion

    #region Fields
    public Action<float> OnProgressChanged;
    #endregion

    #region 싱글톤
    public static ItemDataManager Instance;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }
    #endregion
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

            //www.downloadHandler.text;

            // 데이터 처리 완료
            OnProgressChanged?.Invoke(1.0f);
        }
    }
}
*/