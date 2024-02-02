using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MafiaInteractionController : MonoBehaviour
{
    [SerializeField] int PanelIndex;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            MafiaInteraction.Instance.SetValue(PanelIndex);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MafiaInteraction.Instance.SetValue(100);
        }
    }
}
