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
            if (dishPoint == null)
            {
                dishPoint = sensedUp.collider.gameObject;
                Debug.Log("음식을 놓을 장소의 정보를 가져옵니다.");
            }
            if (changeSprite.spriteRenderer == null)
            {
                changeSprite.CheckTablePoint();
                Debug.Log("랜더러의 위치를 가져옵니다.");
            }
            Debug.Log(sensedUp.collider.gameObject);
            Debug.Log(this.gameObject.name + "은 테이블의 아랫쪽에 있다");
            Debug.DrawRay(transform.position, Vector2.up, new Color(1, 0, 0));
        }
        
    }
    public void SensedBottom()
    {
        RaycastHit2D sensedDown = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, LayerMask.GetMask("Table"));
        if (sensedDown.collider != null)
        {
            if (dishPoint == null)
            {
                dishPoint = sensedDown.collider.gameObject;
                Debug.Log("음식을 놓을 장소의 정보를 가져옵니다.");
            }
            if (changeSprite.spriteRenderer == null)
            {
                changeSprite.CheckTablePoint();
                Debug.Log("랜더러의 위치를 가져옵니다.");
            }
            Debug.Log(this.gameObject.name + "은 테이블의 윗쪽에 있다");
            Debug.DrawRay(transform.position, Vector2.down, new Color(1, 0, 0));
        }
        
    }
    public void SensedLeft()
    {
        RaycastHit2D sensedLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.8f, LayerMask.GetMask("Table"));
        if (sensedLeft.collider != null)
        {
            if (dishPoint == null)
            {
                dishPoint = sensedLeft.collider.gameObject;
                Debug.Log("음식을 놓을 장소의 정보를 가져옵니다.");
            }
            if (changeSprite.spriteRenderer == null)
            {
                changeSprite.CheckTablePoint();
                Debug.Log("랜더러의 위치를 가져옵니다.");
            }
            Debug.Log(this.gameObject.name + "은 테이블의 오른쪽에 있다");
            Debug.DrawRay(transform.position, Vector2.left, new Color(1, 0, 0));
        }
        
    }
    public void SensedRight()
    {
        RaycastHit2D sensedRight = Physics2D.Raycast(transform.position, Vector2.right, 0.8f, LayerMask.GetMask("Table"));
        if (sensedRight.collider != null)
        {
            if (dishPoint == null)
            {
                dishPoint = sensedRight.collider.gameObject;
                Debug.Log("음식을 놓을 장소의 정보를 가져옵니다.");
            }
            if (changeSprite.spriteRenderer == null)
            {
                changeSprite.CheckTablePoint();
                Debug.Log("랜더러의 위치를 가져옵니다.");
            }
            Debug.Log(this.gameObject.name + "은 테이블의 왼쪽에 있다");
            Debug.DrawRay(transform.position, Vector2.right, new Color(1, 0, 0));
        }
        
    }
}
