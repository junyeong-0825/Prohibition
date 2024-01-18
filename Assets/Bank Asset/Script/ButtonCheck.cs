using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonCheck : MonoBehaviour
{
    public delegate void DepositAction(int buttonValue);
    public static event DepositAction onClick;

    public class Constants
    {
        public const int RemittanceValue = 10000;
        public const int DepositValue = 50000;
        public const int LoanValue = 100000;
        public const int WithdrawalValue = 0;
    }
    public void SendValue()
    {
        int buttonValue = 0;

        switch (gameObject.tag)
        {
            case "First":
                buttonValue = Constants.RemittanceValue;
                break;
            case "Second":
                buttonValue = Constants.DepositValue;
                break;
            case "Third":
                buttonValue = Constants.LoanValue;
                break;
            case "Fourth":
                buttonValue = Constants.WithdrawalValue;
                break;
        }
        onClick?.Invoke(buttonValue);
    }
}
