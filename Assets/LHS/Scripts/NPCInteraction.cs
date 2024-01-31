using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject satisfiedSprite;
    public GameObject unsatisfiedSprite;

    public float interactionTimeLimit = 10f;
    public string orderMenu;

    [SerializeField] private bool interactionStarted = false;
    public bool InteractionStarted { get { return interactionStarted; } }
    public bool interactionCompleted = false;
    //public bool InteractionCompleted { get { return interactionCompleted; } }
    private float interactionTimer = 0f;

    private void Start()
    {
        orderMenu = "test";
    }

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
        //Debug.Log("Istart");
    }

    public void DeliverMenu(string deliveredMenu)
    {
        if(!interactionCompleted)
        {
            Debug.Log(deliveredMenu);
            //Debug.Log(orderMenu);
            if(deliveredMenu == orderMenu)
            {
                interactionCompleted = true;
                satisfiedSprite.SetActive(true);
                SelfDestroyTargeting();
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
        SelfDestroyTargeting();
    }

    // 자가 파괴 지정
    private void SelfDestroyTargeting()
    {
        if(interactionCompleted)
        {
            NPCController controller = GetComponent<NPCController>();

            controller.SetTarget(controller.DestroyTarget);
        }
    }
}
