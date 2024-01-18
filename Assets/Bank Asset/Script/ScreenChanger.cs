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
        processData = GetComponent<ProcessData>(); // 이 부분 추가
    }
    public void ChangeButtonScript(int buttonValue)
    {
        if (_button.CompareTag("bank"))
        {
            ButtonText();
            if (buttonValue == 10000)
            {
                _button.tag = "remittance";
                _stateText.text = "송금";
            }
            else if (buttonValue == 50000)
            {
                _button.tag = "deposit";
                _stateText.text = "입금";
            }
            else if (buttonValue == 100000)
            {
                _button.tag = "loan";
                _stateText.text = "대출";
            }
            else if (buttonValue == 0)
            {
                _button.tag = "withdrawal";
                _stateText.text = "출금";
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
        _fourthText.text = "입력 값";
        _firstText.fontSize = fontSize - 16f;
        _secondText.fontSize = fontSize - 16f;
        _thirdText.fontSize = fontSize - 16f;
        _fourthText.fontSize = fontSize - 16f;
    }

    public void BankMainScreen()
    {
        _state.SetActive(false);
        _firstText.text = "송금";
        _secondText.text = "입금";
        _thirdText.text = "대출";
        _fourthText.text = "출금";
        _firstText.fontSize = fontSize;
        _secondText.fontSize = fontSize;
        _thirdText.fontSize = fontSize;
        _fourthText.fontSize = fontSize;
        _button.tag = "bank";
    }
}
