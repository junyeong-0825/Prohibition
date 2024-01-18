using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankHandler : MonoBehaviour
{
    ScreenChanger screenChanger;
    private void Start()
    {
        screenChanger = GetComponent<ScreenChanger>();
    }
    private void OnEnable()
    {
        ButtonCheck.onClick += HandleClick;
    }

    private void OnDisable()
    {
        ButtonCheck.onClick -= HandleClick;
    }

    private void HandleClick(int buttonValue)
    {
        screenChanger.ChangeButtonScript(buttonValue);
    }
}
