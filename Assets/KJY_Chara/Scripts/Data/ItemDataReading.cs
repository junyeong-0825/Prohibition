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
            foreach (Item item in itemSO.itemList.items)
            {
                Sprite sprite = Resources.Load<Sprite>("Sprites/" + item.Name);
                if (sprite != null)
                {
                    item.sprite = sprite;
                }
            }
            //���� ���� �迡 BankData�� ��������
            TextAsset bankData = Resources.Load<TextAsset>("BankData");
            BankData bank = JsonUtility.FromJson<BankData>(bankData.text);
            bankSO.bankData = bank;
            Resources.UnloadUnusedAssets();
            OnProgressChanged?.Invoke(1.0f);
            Debug.Log("Event 2");
        }
    }
}
