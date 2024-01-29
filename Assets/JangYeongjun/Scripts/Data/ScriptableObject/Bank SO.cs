using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BankData
{
    public int Gold;
    public int Debt;
}


[CreateAssetMenu(fileName = "bank", menuName = "Bank", order = 0)]
public class BankSO : ScriptableObject
{
    public BankData bankData;
}
