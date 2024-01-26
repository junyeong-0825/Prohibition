using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemDataReading : MonoBehaviour
{
    public ItemSO itemSO;

    // Start is called before the first frame update
    void Start()
    {
        TextAsset itemFile = Resources.Load<TextAsset>("ItemData");

        if (itemFile != null)
        {
            //Json ���� ������ ���ڿ��� ��������
            string jsonString = itemFile.text;

            //Json ���ڿ��� ��ü�� �Ľ�
            ItemList itemList = JsonUtility.FromJson<ItemList>(jsonString);

            //ItemSO �ʱ�ȭ
            itemSO.itemList = itemList;

            Resources.UnloadUnusedAssets();
        }
    }
    
}