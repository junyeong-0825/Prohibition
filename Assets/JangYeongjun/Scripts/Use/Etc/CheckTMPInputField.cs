using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class CheckTMPInputField : MonoBehaviour
{
    TMP_InputField tmpInputField;
    private void Start()
    {
        tmpInputField = GetComponent<TMP_InputField>();
    }
    public void ClearText()
    {
        if (tmpInputField != null)
        {
            tmpInputField.text = ""; // TMP_InputField�� �ؽ�Ʈ�� ����ϴ�.
        }
    }
}