using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform spawnpoint;
    public MaybeNPC maybeNPC;

    private void Awake()
    {
        spawnpoint = GetComponent<Transform>();
        maybeNPC = GetComponent<MaybeNPC>();
    }
    private void Start()
    {
        SpawnNPC();
    }

    void SpawnNPC()
    {
        GameObject NPC = maybeNPC.SpawnFromPool(0);
        NPC.transform.position = spawnpoint.position;
    }
}
