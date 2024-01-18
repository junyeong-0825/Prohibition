using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MerterialStoreManager : MonoBehaviour
{
    const string MaterialURL = "https://docs.google.com/spreadsheets/d/13vtl_xZLrGFk1j-iw-JTqoMaoQpEFXKu0iNLsITKjyo/export?format=tsv&gid=1341001983&range=A2:C";

    [SerializeField] StoreSO storeSO;
    void Awake()
    {
        StartCoroutine(Get());
    }

    IEnumerator Get()
    {
        UnityWebRequest www = UnityWebRequest.Get(MaterialURL);
        yield return www.SendWebRequest();

        DialogueSO(www.downloadHandler.text);
    }

    void DialogueSO(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowsize = row.Length;
        int columnsize = row[0].Split('\t').Length;

        for (int i = 0; i < rowsize; i++)
        {
            string[] column = row[i].Split('\t');
            for (int j = 0; j < columnsize; j++)
            {
                Store stores = storeSO.store[i];
                stores.name = column[0];
                stores._value = int.Parse(column[1]);
                stores.quantity =int.Parse(column[2]);
            }
        }
    }
}
