using System;
using TMPro;
using UnityEngine;

public class BankInputValueSender : MonoBehaviour
{
    int goldValue;
    [SerializeField] TMP_InputField bankInputField;
    [SerializeField] TextMeshProUGUI warningText;
    public void InputRepayment()
    {
        Int32.TryParse(bankInputField.text, out goldValue);
        if (goldValue > 0)
        {
            BankManager.BankInstance.Repayment(goldValue);
        }
        else
        {
            warningText.text = "입력 값이 1보다 작으면 안됩니다.";
        }
    }
    public void InputLoan()
    {
        Int32.TryParse(bankInputField.text, out goldValue);
        if (goldValue > 0)
        {
            BankManager.BankInstance.Loan(goldValue);
        }
        else
        {
            warningText.text = "입력 값이 1보다 작으면 안됩니다.";
        }
    }

}
