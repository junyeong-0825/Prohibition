using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Drawing;

public class ProcessData : MonoBehaviour
{
    public TextMeshProUGUI _cashText;
    public TextMeshProUGUI _balanceText;
    public TextMeshProUGUI _nameText;
    public TextMeshProUGUI _cardnumberText;

    public void Start()
    {
        DataManager.instance.LoadData();
        Refresh();
        _nameText.text = "Name : " + DataManager.instance.user.name;
        _cardnumberText.text = DataManager.instance.user.cardnumber;
        DataManager.instance.SaveData();
    }
    public string GetThousandCommaText(int data) 
    {
        return string.Format("{0:#,###}", data); 
    }
    public void Refresh()
    {
        _cashText.text = "Cash : " + GetThousandCommaText(DataManager.instance.user.cash);
        _balanceText.text = "Balance : " + GetThousandCommaText(DataManager.instance.user.balance);
    }
    public void DepositProcess(int money)
    {
        DataManager.instance.LoadData();
        if (money > DataManager.instance.user.cash)
        {
            return;
        }
        DataManager.instance.user.cash -= money;
        DataManager.instance.user.balance += money;
        Refresh();
        DataManager.instance.SaveData();
    }
    public  void WithdrawalProcess(int money)
    {
        DataManager.instance.LoadData();
        if (money > DataManager.instance.user.balance)
        {
            return;
        }
        DataManager.instance.user.cash += money;
        DataManager.instance.user.balance -= money;
        Refresh();
        DataManager.instance.SaveData();
    }
}
