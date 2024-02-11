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

    private NavMeshAgent agent;

    private int deployIndex;
    public int DeployIndex { get { return deployIndex; } set { deployIndex = value; } }


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
        SpawnManager.UsedTargetIndex.Remove(index);
    }
}
