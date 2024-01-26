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
        #region �̱���
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
            //Json ���� ������ ���ڿ��� ��������
            string jsonString = itemFile.text;

            //Json ���ڿ��� ��ü�� �Ľ�
            ItemList itemList = JsonUtility.FromJson<ItemList>(jsonString);

            //ItemSO �ʱ�ȭ
            itemSO.itemList = itemList;
            //inventorySO �迭�� ũ�� ����
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
