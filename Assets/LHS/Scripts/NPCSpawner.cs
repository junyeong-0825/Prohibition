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
    public Dictionary<int, bool> EmptySeatCheck = new Dictionary<int, bool>();
    [SerializeField] private Transform selfDestroyPositionP;

    //private List<int> usedTargetIndex = new List<int>();
    //public List<int> UsedTargetIndex { get { return usedTargetIndex; } set { usedTargetIndex = value; } }
    //[SerializeField] private GameObject TargetPrefab;
    private Coroutine spawnCoroutine;

    // 스폰 생성 지연시간
    private float guestInterval = 4.5f;
    //private float policeInterval = 12f;
    private Timer timeLeft;

    void Start()
    {
        SetSeatCheck();
        timeLeft = GameObject.Find("UIManager").GetComponent<Timer>();
        spawnCoroutine = StartCoroutine(spawnNPC());
        //StartCoroutine(spawnNPC(policeInterval, policePrefab, SpawnPositionPrefab));
    }

    internal IEnumerator spawnNPC()
    {

        //// 테스트용
        //while (true)
        //{
        //    if (usedTargetIndex.Count > TargetPrefabList.Count)
        //    {
        //        yield break;
        //    }
        //    if (usedTargetIndex.Count < TargetPrefabList.Count)
        //    {
        //        yield return StartCoroutine(SpawnOnce(guestInterval, guestPrefab, SpawnPositionPrefab));
        //    }
        //    else
        //    {
        //        yield return null;
        //    }
        //}

        //실전용
        while (true)
        {
            //if (usedTargetIndex.Count > TargetPrefabList.Count)
            //{
            //    Debug.Log("Wait Spawn");
            //    yield break;
            //}
            bool isCheck = AreAllValuesFalse(EmptySeatCheck);

            if(timeLeft.limitTimeSec <= 0f)
            {
                Debug.Log("Close Time!!");
                yield return new WaitUntil(() => timeLeft.CheckNPC.Length == 0);
                GameEvents.NotifyDayEnd();
                yield break;
            }

            if (!isCheck)
            {
                Debug.Log("SpawnStart!!!");
                yield return StartCoroutine(SpawnOnce(guestInterval, guestPrefab, SpawnPositionPrefab));
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

    // 프리팹 스폰 메서드(프리팹과 스폰위치를 매개변수로) - 기존 방법
    private void SpawnPrefab(GameObject NPC, Transform Position)
    {
        // int 변수 안에는 빈자리 인덱스에 해당하는 변수를 출력해서 넣는다
        int randomIndex = GetRandomTargetIndex(); 

        // 해당 자리인 게임 오브젝트를 변수 안에 집어 넣는다
        GameObject randomTarget = TargetPrefabList[randomIndex];

        // 스폰하고자 하는 프리팹을 게임 상으로 소환시킨다.
        GameObject newNpc = Instantiate(NPC, Position.position, Quaternion.identity);

        // NPC 내에 엤는 NPCController를 찾아서 넣는다
        NPCController controller = newNpc.transform.Find("MainSprite").GetComponent<NPCController>();

        // NPC의 타겟 정보 및 파괴 위치, 그리고 해당 자리의 인덱스를 넣는다.
        controller.SetTarget(randomTarget.transform);

        controller.DestroyTarget = selfDestroyPositionP;

        controller.DeployIndex = randomIndex;

        // 딕셔너리의 해당 키 인덱스의 값을 false로 바꾼다
        EmptySeatCheck[randomIndex] = true;

        ////사용을 배정받은 인덱스를 더한다.
        //usedTargetIndex.Add(randomIndex);

    }

    private int GetRandomTargetIndex()
    {
        // 지역변수
        int randomIdx;

        //do
        //{
        //    // 타겟 프리팹의 리스트의 수만큼의 랜덤을 구한다.
        //    randomIdx = Random.Range(0, TargetPrefabList.Count);
        //}
        //while (usedTargetIndex.Contains(randomIdx));    // 사용되었던 프리팹의 인덱스 리스트 안에 같은 숫자가 있으면
        //// do while 문은 do 문을 실행시키고 나서 while 조건문이 참이면 계속 실행한다.


        do
        {
            randomIdx = Random.Range(0, TargetPrefabList.Count);
        }
        while (EmptySeatCheck.ContainsKey(randomIdx) && EmptySeatCheck[randomIdx]);

        return randomIdx;
    }

    private bool AreAllValuesFalse(Dictionary<int, bool> dict)
    {
        foreach(var pair in dict)
        {
            if(!pair.Value)
            {
                return false;
            }
        }

        return true;
    }

    private void SetSeatCheck()
    {
        Debug.Log("함수실행?");
        int Count = 0;
        foreach(GameObject Seat in TargetPrefabList)
        {
            EmptySeatCheck.Add(Count, false);
            Count++;
        }
    }
    
}
