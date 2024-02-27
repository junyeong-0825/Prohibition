using UnityEngine;
using TMPro;

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
            tmpInputField.text = ""; // TMP_InputField의 텍스트를 지웁니다.
        }
    }
}