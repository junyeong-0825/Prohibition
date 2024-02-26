using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableRayCast : MonoBehaviour
{
    public bool isChangeBottom = false;
    public bool isChangeTop = false;
    public bool isChangeLeft = false;
    public bool isChangeRight = false;

  
    private void Update()
    {
        SensedBottom();
        SensedLeft();
        SensedTop();
        SensedRight();
    }

    public void SensedTop()
    {
        RaycastHit2D sensedUp = Physics2D.Raycast(transform.position, Vector2.up, 2.2f, LayerMask.GetMask("NPC"));
        if (sensedUp.collider != null)
        {
            isChangeTop = true;
            Debug.Log(this.gameObject.name + "테이블 위");
        }
        else
        {
            isChangeTop = false;
        }
    }
    public void SensedBottom()
    {
        RaycastHit2D sensedDown = Physics2D.Raycast(transform.position, Vector2.down, 2.2f, LayerMask.GetMask("NPC"));
        if (sensedDown.collider != null)
        {
            isChangeBottom = true;
            Debug.Log(this.gameObject.name + "테이블 아래");

        }
        else
        {
            isChangeBottom = false;
        }
    }
    public void SensedLeft()
    {
        RaycastHit2D sensedLeft = Physics2D.Raycast(transform.position, Vector2.left, 3, LayerMask.GetMask("NPC"));
        if (sensedLeft.collider != null)
        {
            isChangeLeft = true;
            Debug.Log(this.gameObject.name + "테이블 왼쪽");
        }
        else
        {
            isChangeLeft = false;
        }
    }
    public void SensedRight()
    {
        RaycastHit2D sensedRight = Physics2D.Raycast(transform.position, Vector2.right, 3, LayerMask.GetMask("NPC"));
        if (sensedRight.collider != null)
        {
            isChangeRight = true;
            Debug.Log(this.gameObject.name + "테이블 오른쪽");
        }
        else
        {
            isChangeRight = false;
        }
    }
}
