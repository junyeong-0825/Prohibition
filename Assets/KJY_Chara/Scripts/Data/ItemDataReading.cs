using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemDataReading : MonoBehaviour
{
    public ItemSO itemSO;
    public static ItemDataReading Instance;
    public Action<float> OnProgressChanged;
    [SerializeField] InventorySO inventorySO;
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
            //inventorySO 배열의 크기 결정
            inventorySO.inventory = new Inventory[itemSO.itemList.items.Count];
            foreach (Item item in itemList.items) 
            {
                Inventory inventorys = new Inventory();
                inventorys.name = item.Name;
            }

            Resources.UnloadUnusedAssets();
            OnProgressChanged?.Invoke(1.0f);
            Debug.Log("Event 2");
        }
    }
    
}
