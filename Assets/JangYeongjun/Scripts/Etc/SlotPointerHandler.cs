using UnityEngine;
using UnityEngine.EventSystems;

public class SlotPointerHandler : MonoBehaviour, IPointerEnterHandler
{
    public TemporaryInventory inventoryItem;

    public void OnPointerEnter(PointerEventData eventData)
    {
        EnhancementChanger enhancementChanger = FindObjectOfType<EnhancementChanger>();
        if (enhancementChanger != null)
        {
            enhancementChanger.ChangeDescription(inventoryItem);
        }
    }
}