using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    // 목표 지정 타겟 변수
    public Transform target;

    // 파괴 위치 지정 변수
    public Transform DestroyTarget;

    // NavMeshAgent를 조작하는 변수
    private NavMeshAgent agent;
    private Vector2 lastPosition;

    // Animation을 조작하기 위한 변수들
    private float X;
    private float Y;
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
        //Vector2 currentVelocity = ((Vector2)transform.position - lastPosition) / Time.deltaTime;

        lastPosition = (Vector2)transform.position;

        if(agent.velocity.sqrMagnitude > 0)
        {
            Vector2 moveDirection = agent.velocity.normalized;

            X = moveDirection.x;
            Y = moveDirection.y;

            //Debug.Log("Move Direction: " + moveDirection);
        }
    }

    private void SetAnimation()
    {
        if(Anim.GetFloat("XVeloValue") != X)
        {
            Anim.SetBool("IsChange", true);
            Anim.SetFloat("XVeloValue", X);
        }
        else if(Anim.GetFloat("YVeloValue") != Y)
        {
            Anim.SetBool("IsChange", true);
            Anim.SetFloat("YVeloValue", Y);
        }
        else
        {
            Anim.SetBool("IsChange", false);
        }
    }

    // 타겟을 조정하는 함수
    public void SetTarget(Transform targetPosition)
    {
        target = targetPosition;
    }

    // 자가파괴 지점에 도착했을 시에 파괴하도록 하는 함수
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Finish")
        {
            Debug.Log("self-Destroy");
            RefreshTargetIndex(deployIndex);
            Destroy(transform.root.gameObject);
        }
    }

    private void RefreshTargetIndex(int index)
    {
        NPCSpawner SpawnManager = GameObject.Find("NPCSpawner").GetComponent<NPCSpawner>();
        SpawnManager.EmptySeatCheck[index] = true;
    }
}
