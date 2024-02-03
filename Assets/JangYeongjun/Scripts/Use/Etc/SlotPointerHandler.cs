using UnityEngine;
using UnityEngine.EventSystems;

public class SlotPointerHandler : MonoBehaviour, IPointerEnterHandler
{
    public Item items;

    public void OnPointerEnter(PointerEventData eventData)
    {
        StoreEnhancer storeEnhancer = FindObjectOfType<StoreEnhancer>();
        if (storeEnhancer != null)
        {
            storeEnhancer.ChangeDescription(items);
        }
    }
}