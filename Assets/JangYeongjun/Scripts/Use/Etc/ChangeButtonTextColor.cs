using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeButtonTextColor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] Color originalColor;
    [SerializeField] Color pressedColor;
    [SerializeField] Color EnterColor;

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonText.color = pressedColor; // 버튼을 누르면 색상 변경
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonText.color = originalColor; // 버튼에서 손을 떼면 원래 색상으로 복원
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = EnterColor; // 버튼에 올라가면 색상 변경
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = originalColor; // 버튼에서 나가면 색상 변경
    }
}
