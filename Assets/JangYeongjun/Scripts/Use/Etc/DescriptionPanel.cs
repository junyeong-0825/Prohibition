using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionPanel : MonoBehaviour
{
    [SerializeField] TMP_Text TitleText;
    [SerializeField] TMP_Text descriptText;

    private void Start()
    {
        HidePanel();
    }

    public void ShowPanel(string header, string description)
    {
        TitleText.text = header;
        descriptText.text = description;
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
