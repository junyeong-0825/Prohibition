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

    public void OnInteraction(InputValue value)
    {
        if(currentNPCInteraction != null && value.isPressed)
        {
            DeliverMenuToGuest();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Guest") && currentNPCInteraction.InteractionStarted)
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

    private void DeliverMenuToGuest()
    {
        if(currentNPCInteraction != null && currentNPCInteraction.interactionCompleted)
        {
            string deliverMenu = "1";

            currentNPCInteraction.DeliverMenu(deliverMenu);
        }
    }
}
