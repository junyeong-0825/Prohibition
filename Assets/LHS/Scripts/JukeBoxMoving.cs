using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeBoxMoving : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float moveDistance = 0.7f;
    private PlayerStatus playerStatus;
    private bool isUndercover;
    private bool isMoving = false;
    private Vector2 initialPosition;
    private Vector2 targetPosition;

    private void Start()
    {
        playerStatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
        if(playerStatus == null)
        {
            Debug.LogError("PlayerStatus를 찾을 수 없습니다.");
        }

        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStatus != null)
        {
            isUndercover = playerStatus.isUndercover;
            if(isUndercover && !isMoving)
            {
                targetPosition = (Vector2)initialPosition + Vector2.left * moveDistance;
                isMoving = true;
                //StartCoroutine(MoveLeft());
            }
            else if(!isUndercover && !isMoving)
            {
                targetPosition = initialPosition;
                isMoving = true;
                //StartCoroutine(MoveRight());
            }
            if(isMoving)
            {
                MoveToTarget();
            }
        }
    }

    void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, MoveSpeed * Time.deltaTime);

        if((Vector2)transform.position == targetPosition)
        {
            isMoving = false;
        }
    }
}
