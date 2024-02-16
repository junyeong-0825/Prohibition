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

    #region �̱���
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
            // ������ ó�� ����
            OnProgressChanged?.Invoke(0.7f);

            //www.downloadHandler.text;

            // ������ ó�� �Ϸ�
            OnProgressChanged?.Invoke(1.0f);
        }
    }
}
*/