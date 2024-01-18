using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Drawing;

public class ScreenChanger : MonoBehaviour
{
    public TextMeshProUGUI _firstText;
    public TextMeshProUGUI _secondText;
    public TextMeshProUGUI _thirdText;
    public TextMeshProUGUI _fourthText;
    public TextMeshProUGUI _stateText;
    public TMP_InputField _inputValue;
    public GameObject _button;
    public GameObject _state;
    public float fontSize = 36f;
    ProcessData processData;
    private void Start()
    {
        processData = GetComponent<ProcessData>(); // �� �κ� �߰�
    }
    public void ChangeButtonScript(int buttonValue)
    {
        if (_button.CompareTag("bank"))
        {
            ButtonText();
            if (buttonValue == 10000)
            {
                _button.tag = "remittance";
                _stateText.text = "�۱�";
            }
            else if (buttonValue == 50000)
            {
                _button.tag = "deposit";
                _stateText.text = "�Ա�";
            }
            else if (buttonValue == 100000)
            {
                _button.tag = "loan";
                _stateText.text = "����";
            }
            else if (buttonValue == 0)
            {
                _button.tag = "withdrawal";
                _stateText.text = "���";
            }
        }
        else if(_button.CompareTag("deposit"))
        {
            if(buttonValue == 0)
            {
                processData.DepositProcess(buttonValue+ int.Parse(_inputValue.text));
            }
            else {processData.DepositProcess(buttonValue);}
            
        }
        else if (_button.CompareTag("withdrawal"))
        {
            if (buttonValue == 0)
            {
                processData.WithdrawalProcess(buttonValue + int.Parse(_inputValue.text));
            }
            processData.WithdrawalProcess(buttonValue);
        }
    }
    public void ButtonText()
    {
        _state.SetActive(true);
        _firstText.text = "10,000";
        _secondText.text = "50,000";
        _thirdText.text = "100,000";
        _fourthText.text = "�Է� ��";
        _firstText.fontSize = fontSize - 16f;
        _secondText.fontSize = fontSize - 16f;
        _thirdText.fontSize = fontSize - 16f;
        _fourthText.fontSize = fontSize - 16f;
    }

    public void BankMainScreen()
    {
        _state.SetActive(false);
        _firstText.text = "�۱�";
        _secondText.text = "�Ա�";
        _thirdText.text = "����";
        _fourthText.text = "���";
        _firstText.fontSize = fontSize;
        _secondText.fontSize = fontSize;
        _thirdText.fontSize = fontSize;
        _fourthText.fontSize = fontSize;
        _button.tag = "bank";
    }
}
