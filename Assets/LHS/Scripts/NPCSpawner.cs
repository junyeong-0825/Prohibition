using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCSpawner : MonoBehaviour
{
   
    // 오브젝트를 넣을 프리팹
    [SerializeField] private GameObject guestPrefab;
    [SerializeField] private GameObject policePrefab;

    // 오브젝트 스폰과 파괴 위치값
    [SerializeField] private Transform SpawnPositionPrefab;
    [SerializeField] private List<GameObject> TargetPrefabList;

    private List<int> usedTargetIndex = new List<int>();
    //[SerializeField] private GameObject TargetPrefab;
    private Coroutine spawnCoroutine;

    // 스폰 생성 지연시간
    private float guestInterval = 4.5f;
    //private float policeInterval = 12f;

    void Start()
    {
        spawnCoroutine = StartCoroutine(spawnNPC(guestInterval, guestPrefab, SpawnPositionPrefab));
        //StartCoroutine(spawnNPC(policeInterval, policePrefab, SpawnPositionPrefab));
    }
    
    private IEnumerator spawnNPC(float interval, GameObject NPC, Transform Position)
    {
        while(usedTargetIndex.Count < TargetPrefabList.Count)
        {
            yield return new WaitForSeconds(interval);
            SpawnPrefab(NPC, Position);
        }

        StopCoroutine(spawnCoroutine);
    }

    private void SpawnPrefab(GameObject NPC, Transform Position)
    {
        int randomIndex = GetRandomTargetIndex(); 

        GameObject randomTarget = TargetPrefabList[randomIndex];

        GameObject newNpc = Instantiate(NPC, Position.position, Quaternion.identity);

        NPCController controller = newNpc.transform.Find("Body_1").GetComponent<NPCController>();

        controller.SetTarget(randomTarget.transform);

        usedTargetIndex.Add(randomIndex);

    }

    private int GetRandomTargetIndex()
    {
        int randomIdx;

        do
        {
            randomIdx = Random.Range(0, TargetPrefabList.Count);
        }
        while (usedTargetIndex.Contains(randomIdx));

        return randomIdx;
    }
    
}
