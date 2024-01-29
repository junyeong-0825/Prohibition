using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemDataReading : MonoBehaviour
{
    public ItemSO itemSO;
    public BankSO bankSO;
    public static ItemDataReading Instance;
    public Action<float> OnProgressChanged;
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
        TextAsset itemFile = Resources.Load<TextAsset>("ItemData");
        OnProgressChanged?.Invoke(0.7f);
        Debug.Log("Event 1");
        if (itemFile != null)
        {
            //Json 파일 내용을 문자열로 가져오기
            string jsonString = itemFile.text;

            //Json 문자열을 객체로 파싱
            ItemList itemList = JsonUtility.FromJson<ItemList>(jsonString);

            //ItemSO 초기화
            itemSO.itemList = itemList;
            foreach (Item item in itemSO.itemList.items)
            {
                Sprite sprite = Resources.Load<Sprite>("Sprites/" + item.Name);
                if (sprite != null)
                {
                    item.sprite = sprite;
                }
            }
            //가져 오는 김에 BankData도 가져오기
            TextAsset bankData = Resources.Load<TextAsset>("BankData");
            BankData bank = JsonUtility.FromJson<BankData>(bankData.text);
            bankSO.bankData = bank;
            Resources.UnloadUnusedAssets();
            OnProgressChanged?.Invoke(1.0f);
            Debug.Log("Event 2");
        }
    }
}
