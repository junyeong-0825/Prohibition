using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankDataManager : MonoBehaviour
{
    public void SetStart()
    {
        TextAsset bankData = Resources.Load<TextAsset>("BankData");
        BankSO bank = JsonUtility.FromJson<BankSO>(bankData.text);
    }
}
