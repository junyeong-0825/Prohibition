using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabToInput : MonoBehaviour
{
    public TMP_InputField IDInput, PassInput;
    int InputSelect;
    #region InputField를 Tab으로 이동하는 로직
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InputSelect++;
            if (InputSelect > 1) { InputSelect = 0; }
            SelectInputField();
        }
        void SelectInputField()
        {
            if (InputSelect == 0) { IDInput.Select(); }
            else if (InputSelect == 1) { PassInput.Select(); }
        }
    }
    public void IDInputSelected() => InputSelect = 0;
    public void PasswordSelected() => InputSelect = 1;
    #endregion
}
