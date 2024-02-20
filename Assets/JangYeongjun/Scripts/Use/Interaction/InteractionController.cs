using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            InteractionManager.interactionInstance.SetValue(gameObject.name);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InteractionManager.interactionInstance.SetValue("None");
        }
    }
}
