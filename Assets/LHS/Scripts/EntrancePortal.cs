using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrancePortal : MonoBehaviour
{
    private Transform destination;

    public bool IsInside;
    public float distance = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        // 이 스크립트를 적용하는 오브젝트가 해당하는 입구가 내부인지 외부인지 게임 태그로 확인
        if (IsInside == false)
        {
            // OutsideEntrance 오브젝트가 내부 입구의 트랜스폼의 좌표값을 destination 변수에 선언한다.
            destination = GameObject.FindGameObjectWithTag("InsideEntrance").GetComponent<Transform>();
        }
        else
        {
            // InsideEntrance의 오브젝트가 외부 입구의 트랜스폼의 좌표값을 destination 변수에 선언한다.
            destination = GameObject.FindGameObjectWithTag("OutsideEntrance").GetComponent<Transform>();
        }
    }

    // 충돌하는 모든 오브젝트들
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Guest"))
        {
            bool checkInteractionStart = other.GetComponent<NPCInteraction>().InteractionStarted;
            bool checkInteractionCompleted = other.GetComponent<NPCInteraction>().interactionCompleted;
            if(!checkInteractionStart)
            {
                teleport(other);
            }
            else if(checkInteractionCompleted)
            {
                teleport(other);
            }
        }
    }

    // 이 스크립트에 적용된 오브젝트와 충돌하는 오브젝트의 위치간의 거리가 distance 보다 크다면 해당 충돌 오브젝트는 순간이동을 한다.
    private void teleport(Collider2D collision)
    {
        if (Vector2.Distance(transform.position, collision.transform.position) > distance)
        {

            collision.transform.position = new Vector2(destination.position.x, destination.position.y);
        }
    }
}
