using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeButtonTextColor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] Color originalColor;
    [SerializeField] Color pressedColor;
    [SerializeField] Color EnterColor;

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonText.color = pressedColor; // ��ư�� ������ ���� ����
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonText.color = originalColor; // ��ư���� ���� ���� ���� �������� ����
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = EnterColor; // ��ư�� �ö󰡸� ���� ����
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = originalColor; // ��ư���� ������ ���� ����
    }
}
