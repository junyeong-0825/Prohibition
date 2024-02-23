using DarkPixelRPGUI.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescriptionInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] DescriptionPanel descriptionPanel;
    [SerializeField] string title;
    [SerializeField] string description;

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionPanel.ShowPanel(title, description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionPanel.HidePanel();
    }
}
