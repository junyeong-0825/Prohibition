using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankButtonValueSender : MonoBehaviour
{
    [SerializeField] int goldValue;
    
    public void OnRepayment()
    {
        BankManager.BankInstance.Repayment(goldValue);
    }
    public void OnLoan()
    {
        BankManager.BankInstance.Loan(goldValue);
    }
}
