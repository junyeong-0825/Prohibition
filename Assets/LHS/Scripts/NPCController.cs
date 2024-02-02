using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public Transform target;

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
        agent.SetDestination(target.position);
    }

    public void SetTarget(Transform targetPosition)
    {
        target = targetPosition;
    }

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
