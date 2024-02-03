using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InOutCameraMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Vector3 inPosition;
    [SerializeField] Vector3 outPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        mainCamera.transform.position = outPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mainCamera.transform.position = inPosition;
    }
}

