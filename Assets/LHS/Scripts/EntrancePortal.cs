using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntrancePortal : MonoBehaviour
{
    // 상대편 포탈로 이동할 수 있게 하는 좌표값
    private Transform destination;

    private Transform insideEntrance;
    private Transform outsideEntrance;

    public bool IsInside;
    public float distance = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        // 이 스크립트를 적용하는 오브젝트가 해당하는 입구가 내부인지 외부인지 게임 태그로 확인
        if (IsInside == false)
        {
            // OutsideEntrance 오브젝트가 내부 입구의 트랜스폼의 좌표값을 destination 변수에 선언한다.
            // 동시에 이 스크립트가 적용된 포탈은 식당 외부 입구 오브젝트인 것을 알 수 있다.
            destination = GameObject.FindGameObjectWithTag("InsideEntrance").GetComponent<Transform>();
        }
        else
        {
            // InsideEntrance의 오브젝트가 외부 입구의 트랜스폼의 좌표값을 destination 변수에 선언한다.
            // 동시에 이 스크립트가 적용된 포탈은 식당 내부 입구 오브젝트인 것을 알 수 있다.
            destination = GameObject.FindGameObjectWithTag("OutsideEntrance").GetComponent<Transform>();
        }
    }

    // 충돌하는 모든 오브젝트들
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Guest"))
        {
            bool checkInteractionStart = other.GetComponent<NPCInteraction>().InteractionStarted;
            bool checkInteractionCompleted = other.GetComponent<NPCInteraction>().InteractionCompleted;
            NPCController controller = other.GetComponent<NPCController>();

            if (!checkInteractionStart && !IsInside)
            {
                controller.nextTarget = destination;
                controller.SetTarget(controller.seatTarget);
                teleport(other);
            }
            else if(checkInteractionCompleted && IsInside)
            {
                controller.SetTarget(controller.DestroyTarget);
                teleport(other);
            }
        }

        else if(other.CompareTag("Police"))
        {
            // 경찰이 진입에 들어가면 다른 상태로 가는
        }
    }

    // 이 스크립트에 적용된 오브젝트와 충돌하는 오브젝트의 위치간의 거리가 distance 보다 크다면 해당 충돌 오브젝트는 순간이동을 한다.
    private void teleport(Collider2D collision)
    {
        if (Vector2.Distance(transform.position, collision.transform.position) > distance)
        {
            NavMeshAgent agent = collision.GetComponent<NavMeshAgent>();
            agent.Warp(destination.position);
            //collision.transform.position = new Vector2(destination.position.x, destination.position.y);
        }
    }
}
