using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlcoholPanelCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI beerCountText;
    [SerializeField] private TextMeshProUGUI wineCountText;
    [SerializeField] private TextMeshProUGUI whiskyCountText;

    private void OnEnable()
    {
        UpdateAlcoholCount("Beer", beerCountText);
        UpdateAlcoholCount("Wine", wineCountText);
        UpdateAlcoholCount("Whisky", whiskyCountText);
    }

    private void UpdateAlcoholCount(string alcoholName, TextMeshProUGUI textComponent)
    {
        PlayerInventory inventoryItem = DataManager.instance.nowPlayer.inventory.Find(inven => inven.Name == alcoholName);
        if (inventoryItem != null)
        {
            textComponent.text = inventoryItem.Quantity.ToString();
        }
        else
        {
            textComponent.text = "0";
        }
    }
}
