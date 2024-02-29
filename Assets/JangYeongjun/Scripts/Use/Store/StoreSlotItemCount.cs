using TMPro;
using UnityEngine;

public class StoreSlotItemCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemCount;
    [SerializeField] TextMeshProUGUI nameText;
    PlayerInventory beerInven;
    PlayerInventory wineInven;
    PlayerInventory whiskyInven;
    PlayerInventory foodInven;
    private void Start()
    {
        FindInvenItem();
    }
    private void OnEnable()
    {
        FindInvenItem();
        GameEvents.OnInventoryChanged += FindInvenItem;
    }
    private void OnDisable()
    {
        GameEvents.OnInventoryChanged -= FindInvenItem;
    }

    void FindInvenItem()
    {
        if (nameText.text == "Beer")
        {
            beerInven = DataManager.instance.nowPlayer.inventory.Find(inven => inven.Name == "Beer");
            if (beerInven != null) { itemCount.text = beerInven.Quantity.ToString(); }
            else { itemCount.text = "0"; }
        }
        else if (nameText.text == "Wine")
        {
            wineInven = DataManager.instance.nowPlayer.inventory.Find(inven => inven.Name == "Wine");
            if (wineInven != null) { itemCount.text = wineInven.Quantity.ToString(); }
            else { itemCount.text = "0"; ; }
        }
        else if (nameText.text == "Whisky")
        {
            whiskyInven = DataManager.instance.nowPlayer.inventory.Find(inven => inven.Name == "Whisky");
            if (whiskyInven != null) { itemCount.text = whiskyInven.Quantity.ToString(); }
            else { itemCount.text = "0"; }
        }
        else if (nameText.text == "Food")
        {
            foodInven = DataManager.instance.nowPlayer.inventory.Find(inven => inven.Name == "Food");
            if (foodInven != null) { itemCount.text = foodInven.Quantity.ToString(); }
            else { itemCount.text = "0"; }
        }
    }
}
