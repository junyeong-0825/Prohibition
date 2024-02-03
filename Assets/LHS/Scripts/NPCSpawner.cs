using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class GameEvents
{
    public delegate void DayEndDelegate();
    public static event DayEndDelegate OnDayEnd;

    public static void NotifyDayEnd()
    {
        if (OnDayEnd != null)
        {
            OnDayEnd();
        }
    }
}
public class NPCSpawner : MonoBehaviour
{
   
    // 오브젝트를 넣을 프리팹
    [SerializeField] private GameObject guestPrefab;
    [SerializeField] private GameObject policePrefab;

    // 오브젝트 스폰과 파괴 위치값
    [SerializeField] private Transform SpawnPositionPrefab;
    [SerializeField] private List<GameObject> TargetPrefabList;
    [SerializeField] private Transform selfDestroyPositionP;

    private List<int> usedTargetIndex = new List<int>();
    public List<int> UsedTargetIndex { get { return usedTargetIndex; } set { usedTargetIndex = value; } }
    //[SerializeField] private GameObject TargetPrefab;
    private Coroutine spawnCoroutine;

    // 스폰 생성 지연시간
    private float guestInterval = 4.5f;
    //private float policeInterval = 12f;
    private Timer timeLeft;
    public float currenttime;


    private void Awake()
    {
        SetTime();
    }

    void Start()
    {
        spawnCoroutine = StartCoroutine(spawnNPC(guestInterval, guestPrefab, SpawnPositionPrefab));
        //StartCoroutine(spawnNPC(policeInterval, policePrefab, SpawnPositionPrefab));
    }

    private IEnumerator spawnNPC(float interval, GameObject NPC, Transform Position)
    {
  

        //while (true)
        //{
        //    if(usedTargetIndex.Count > TargetPrefabList.Count)
        //    {
        //        yield break;
        //    }
        //    if(usedTargetIndex.Count < TargetPrefabList.Count)
        //    {
        //        yield return StartCoroutine(SpawnOnce(interval, NPC, Position));
        //    }
        //    else
        //    {
        //        yield return null;
        //    }
        //}

        while(currenttime > 0f)
        {
            SetTime();
            //if (usedTargetIndex.Count > TargetPrefabList.Count)
            //{
            //    Debug.Log("Wait Spawn");
            //    yield break;
            //}

            if(currenttime <= 0f)
            {
                Debug.Log("Close Time!!");
                yield return new WaitUntil(() => timeLeft.CheckNPC.Length == 0);
                GameEvents.NotifyDayEnd();
                yield break;
            }

            if((usedTargetIndex.Count < TargetPrefabList.Count))
            {
                Debug.Log("SpawnStart!!!");
                yield return StartCoroutine(SpawnOnce(interval, NPC, Position));
            }
            else
            {
                yield return null;
            }
        }
    }

    private IEnumerator SpawnOnce(float interval, GameObject NPC, Transform Position)
    {
        yield return new WaitForSeconds(interval);
        SpawnPrefab(NPC, Position);
    }

    private void SetTime()
    {
        timeLeft = GameObject.Find("UIManager").GetComponent<Timer>();
        currenttime = timeLeft.LimitTimeSec;
    }

    // 프리팹 스폰 메서드(프리팹과 스폰위치를 매개변수로)
    private void SpawnPrefab(GameObject NPC, Transform Position)
    {
        int randomIndex = GetRandomTargetIndex(); 

        GameObject randomTarget = TargetPrefabList[randomIndex];

        GameObject newNpc = Instantiate(NPC, Position.position, Quaternion.identity);

        NPCController controller = newNpc.transform.Find("Body_1").GetComponent<NPCController>();

        controller.SetTarget(randomTarget.transform);

        controller.DestroyTarget = selfDestroyPositionP;

        controller.DeployIndex = randomIndex;

        usedTargetIndex.Add(randomIndex);

    }

    private int GetRandomTargetIndex()
    {
        // 지역변수
        int randomIdx;

        do
        {
            // 타겟 프리팹의 리스트의 수만큼의 랜덤을 구한다.
            randomIdx = Random.Range(0, TargetPrefabList.Count);
        }
        while (usedTargetIndex.Contains(randomIdx));    // 사용되었던 프리팹의 인덱스 리스트 안에 같은 숫자가 있으면

        return randomIdx;
    }
    
}
