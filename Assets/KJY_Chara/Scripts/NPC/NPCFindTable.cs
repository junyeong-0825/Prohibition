using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFindTable : MonoBehaviour
{

    public GameObject dishPoint;
    public ChangeSprite changeSprite;

    private void Awake()
    {
        changeSprite = GetComponent<ChangeSprite>();
    }
    private void Update()
    {
        SensedBottom();
        SensedLeft();
        SensedTop();
        SensedRight();
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, Vector2.up, new Color(1, 0, 0));
        Debug.DrawRay(transform.position, Vector2.down, new Color(1, 0, 0));
        Debug.DrawRay(transform.position, Vector2.left, new Color(1, 0, 0));
        Debug.DrawRay(transform.position, Vector2.right, new Color(1, 0, 0));
    }
    public void SensedTop()
    {
        RaycastHit2D sensedUp = Physics2D.Raycast(transform.position, Vector2.up, 1.4f, LayerMask.GetMask("Table"));
        if (sensedUp.collider != null)
        {
            dishPoint = sensedUp.collider.gameObject;
            changeSprite.CheckTablePoint();
        }
        
    }
    public void SensedBottom()
    {
        RaycastHit2D sensedDown = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, LayerMask.GetMask("Table"));
        if (sensedDown.collider != null)
        {
            dishPoint = sensedDown.collider.gameObject;
            changeSprite.CheckTablePoint();
        }
        
    }
    public void SensedLeft()
    {
        RaycastHit2D sensedLeft = Physics2D.Raycast(transform.position, Vector2.left, 1, LayerMask.GetMask("Table"));
        if (sensedLeft.collider != null)
        {
            dishPoint = sensedLeft.collider.gameObject;
            changeSprite.CheckTablePoint();
        }
        
    }
    public void SensedRight()
    {
        RaycastHit2D sensedRight = Physics2D.Raycast(transform.position, Vector2.right, 1, LayerMask.GetMask("Table"));
        if (sensedRight.collider != null)
        {
            dishPoint = sensedRight.collider.gameObject;
            changeSprite.CheckTablePoint();
        }
        
    }
}
