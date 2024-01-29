using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public string orderMenu = "1";
    public GameObject satisfiedSprite;
    public GameObject unsatisfiedSprite;

    public float interactionTimeLimit = 10f;

    [SerializeField] private bool interactionStarted = false;
    public bool InteractionStarted { get { return interactionStarted; } }
    public bool interactionCompleted = false;
    //public bool InteractionCompleted { get { return interactionCompleted; } }
    private float interactionTimer = 0f;


    // Update is called once per frame
    void Update()
    {
        //if(player_detection && )
        if(interactionStarted && !interactionCompleted)
        {
            
            interactionTimer += Time.deltaTime;
            //Debug.Log(interactionTimer);
            if (interactionTimer >= interactionTimeLimit)
            {
                HandleInteractionFailed();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EmptyChair") && !interactionStarted && !interactionCompleted)
        {
            //Debug.Log("StartInteraction");
            StartInteraction();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("EmptyChair") && interactionStarted && !interactionCompleted)
        {
            //StartInteraction();
        }
    }

    private void StartInteraction()
    {
        interactionStarted = true;
    }

    public void DeliverMenu(string deliveredMenu)
    {
        if(!interactionCompleted)
        {
            if(deliveredMenu == orderMenu)
            {
                interactionCompleted = true;
                satisfiedSprite.SetActive(true);
            }
            else
            {
                HandleInteractionFailed();
            }
        }
    }

    private void HandleInteractionFailed()
    {
        interactionCompleted = true;
        unsatisfiedSprite.SetActive(true);
    }
}
