using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : CharaController
{
    private Camera _camera;
    private NPCInteraction currentNPCInteraction;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Guest"))
        {
            currentNPCInteraction = other.GetComponent<NPCInteraction>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Guest"))
        {
            currentNPCInteraction = null;
        }
    }

    public void OnInteraction(InputValue value)
    {
        if (currentNPCInteraction != null && value.isPressed && currentNPCInteraction.InteractionStarted)
        {
            Debug.Log("interaction is success");
            DeliverMenuToGuest();
        }
    }

    private void DeliverMenuToGuest()
    {
        if(currentNPCInteraction != null && currentNPCInteraction.InteractionStarted)
        {
            string deliverMenu = "test";

            currentNPCInteraction.DeliverMenu(deliverMenu);
            Debug.Log(currentNPCInteraction.orderMenu);
        }
    }
}
