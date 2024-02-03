using System;
using System.Collections;
using System.Collections.Generic;
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
            Debug.Log($"{goldValue}�� {bankInputField.text}");
            BankManager.BankInstance.Repayment(goldValue);
        }
        else
        {
            warningText.text = "�Է� ���� 1���� ������ �ȵ˴ϴ�.";
        }
    }
    public void InputLoan()
    {
        Int32.TryParse(bankInputField.text, out goldValue);
        if (goldValue > 0)
        {
            Debug.Log($"{goldValue}�� {bankInputField.text}");
            BankManager.BankInstance.Loan(goldValue);
        }
        else
        {
            warningText.text = "�Է� ���� 1���� ������ �ȵ˴ϴ�.";
        }
    }
}
