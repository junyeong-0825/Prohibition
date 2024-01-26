using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    // 오브젝트를 넣을 프리팹
    [SerializeField] private GameObject guestPrefab;
    [SerializeField] private GameObject policePrefab;
    [SerializeField] private GameObject passPrefab;

    // 오브젝트 스폰과 파괴 위치값
    [SerializeField] private Transform SpawnPositionPrefab;
    [SerializeField] private Transform DestroyPositionPrefab;

    // 스폰 생성 지연시간
    private float guestInterval = 4.5f;
    private float policeInterval = 12f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnNPC(guestInterval, guestPrefab, SpawnPositionPrefab));
        StartCoroutine(spawnNPC(policeInterval, policePrefab, SpawnPositionPrefab));
    }

    private IEnumerator spawnNPC(float interval, GameObject NPC, Transform Position)
    {
        yield return new WaitForSeconds(interval);
        GameObject newNpc = Instantiate(NPC, Position.position, Quaternion.identity);
        StartCoroutine(spawnNPC(interval, NPC, Position));
    }
}
