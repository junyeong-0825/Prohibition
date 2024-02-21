using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    // 목표 지정 타겟 변수
    public Transform target;

    // 좌석 정보가 담긴 타겟 변수(손님만 할당)
    public Transform seatTarget;

    // 다음 타겟 정보를 저장하는 변수
    public Transform nextTarget;

    // 파괴 위치 지정 변수
    public Transform DestroyTarget;

    // NavMeshAgent를 조작하는 변수
    private NavMeshAgent agent;
    private Vector2 lastPosition;

    // Animation을 조작하기 위한 변수들
    private float XVeloFloat;
    private float YVeloFloat;
    private Animator Anim;

    private int deployIndex;
    public int DeployIndex { get { return deployIndex; } set { deployIndex = value; } }

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }


    void Update()
    {
        // 목표로 이동시키도록 하는 메서드
        agent.SetDestination(target.position);
        MoveDirection();
        SetAnimation();
    }


    private void MoveDirection()
    {
        lastPosition = (Vector2)transform.position;
        Vector2 moveDirection = agent.velocity.normalized;

        XVeloFloat = moveDirection.x;
        YVeloFloat = moveDirection.y;

        //Debug.Log("Move Direction: " + moveDirection);


        //if(agent.velocity.sqrMagnitude > 0)
        //{
        //    Vector2 moveDirection = agent.velocity.normalized;

        //    XVeloFloat = moveDirection.x;
        //    YVeloFloat = moveDirection.y;

        //    Debug.Log("Move Direction: " + moveDirection);
        //    Debug.Log("Move DirectionX: " + XVeloFloat);
        //    Debug.Log("Move DirectionY: " + YVeloFloat);
        //}
    }

    private void SetAnimation()
    {
        float XAbs = Mathf.Abs(XVeloFloat);
        float YAbs = Mathf.Abs(YVeloFloat);

        if (agent.remainingDistance == 0)
        {
            Anim.SetBool("IsChange", false);
        }

        if (Anim.GetFloat("XVeloValue") != XVeloFloat)
        {
            Anim.SetBool("IsChange", true);
            Anim.SetFloat("XVeloValue", XVeloFloat);
            //Debug.Log("destination " + agent.remainingDistance);
        }
        else if (Anim.GetFloat("YVeloValue") != YVeloFloat)
        {
            Anim.SetBool("IsChange", true);
            Anim.SetFloat("YVeloValue", YVeloFloat);

        }
        else if (XAbs >= YAbs)
        {
            Anim.SetBool("IsXBig", true);
            Anim.SetBool("IsYBig", false);
        }
        else if (YAbs > XAbs)
        {
            Anim.SetBool("IsXBig", false);
            Anim.SetBool("IsYBig", true);
        }
    }

    // 타겟을 조정하는 함수
    public void SetTarget(Transform targetPosition)
    {
        target = targetPosition;
    }

    // 충돌 콜라이더와 충돌했을 때의 메서드
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Finish" && gameObject.tag == "Guest")
        {
            //Debug.Log("self-Destroy");
            RefreshTargetIndex(deployIndex);
            Destroy(transform.root.gameObject);
        }

        else if(other.gameObject.tag == "Finish" && gameObject.tag == "Police")
        {
            Destroy(transform.root.gameObject);
        }

        //else if(other.gameObject.tag == "OutsideEntrance" && !checkInteractionStart)
        //{
        //    SetTarget(seatTarget);
        //}
    }

    // 딕셔너리를 조정하는 메서드지만 NPC 인스턴스가 실행하는 용도는 아닌거 같다
    private void RefreshTargetIndex(int index)
    {
        NPCSpawner SpawnManager = GameObject.Find("NPCSpawner").GetComponent<NPCSpawner>();
        SpawnManager.EmptySeatCheck[index] = false;
    }
}
