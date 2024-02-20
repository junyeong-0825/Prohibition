using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCSpawner : MonoBehaviour
{
   
    // 오브젝트를 넣을 프리팹과 스프라이트
    [SerializeField] private GameObject[] guestPrefab;
    [SerializeField] private Sprite[] menuSprite;
    [SerializeField] private GameObject policePrefab;

    // 오브젝트 스폰과 파괴 위치값
    [SerializeField] private Transform SpawnPositionPrefab;
    // 식당 입구의 위치값을 가지는 변수값
    [SerializeField] private Transform EntranceTargetObject;
    [SerializeField] private List<GameObject> TargetPrefabList; //
    public Dictionary<int, bool> EmptySeatCheck = new Dictionary<int, bool>(); //
    [SerializeField] private Transform selfDestroyPositionP; //

    //[SerializeField] private GameObject TargetPrefab;
    private Coroutine spawnCoroutine;

    // 스폰 생성 지연시간
    private float guestInterval = 1.5f;
    //private float policeInterval = 12f;
    private Timer timeLeft;

    void Start()
    {
        SetSeatCheck();
        timeLeft = GameObject.Find("UIManager").GetComponent<Timer>();
        //spawnCoroutine = StartCoroutine(spawnNPC());
        //StartCoroutine(spawnNPC(policeInterval, policePrefab, SpawnPositionPrefab));
    }


    // 경찰을 스폰하는 메서드
    // 조건 : 제한 시간 내에 스폰 횟수를 충족시켜야 함, 경찰은 한 스테이지 당 총 3번 스폰된다, 스폰 되면 다른 경찰을 스폰할 수 없다, 
    //internal IEnumerator spawnPolice()
    //{
    //    if
    //    while(true)
    //    {

    //    }

    //    yield return null;
    //}

    // 일반 NPC를 스폰하는 코루틴 메서드
    internal IEnumerator spawnNPC()
    {
        //실전용
        while (true)
        {
            //if (usedTargetIndex.Count > TargetPrefabList.Count)
            //{
            //    Debug.Log("Wait Spawn");
            //    yield break;
            //}
            // 빈자리 수만큼 NPC가 스폰되었다면 스폰 상태를 체크하는 bool값 선언
            bool isCheck = AreAllValuesFalse(EmptySeatCheck);
            Debug.Log(isCheck);

            if(timeLeft.limitTimeSec <= 0f)
            {
                Debug.Log("Close Time!!");
                yield return new WaitUntil(() => timeLeft.CheckNPC.Length == 0);
                GameEvents.NotifyDayEnd();
                yield break;
            }

            if (!isCheck)
            {
                int index = UnityEngine.Random.Range(0, guestPrefab.Length);
                Debug.Log(index);
                Debug.Log("SpawnStart!!!");
                yield return SpawnOnce(guestInterval, guestPrefab[index], SpawnPositionPrefab);
            }
            else
            {
                yield return null;
            }
        }
    }

    private IEnumerator SpawnOnce(float interval, GameObject NPC, Transform Position)
    {
        Debug.Log("Coroutine Start");
        yield return new WaitForSeconds(interval);
        SpawnPrefab(NPC, Position);
    }

    // 프리팹 스폰 메서드(프리팹과 스폰위치를 매개변수로) - 기존 방법
    private void SpawnPrefab(GameObject NPC, Transform Position)
    {
        Debug.Log("Drop Prefab");
        // int 변수 안에는 빈자리 인덱스에 해당하는 변수를 출력해서 넣는다
        int randomIndex = GetRandomTargetIndex(); 

        // 해당 자리인 게임 오브젝트를 변수 안에 집어 넣는다
        GameObject randomTarget = TargetPrefabList[randomIndex];

        // 스폰하고자 하는 프리팹을 게임 상으로 소환시킨다.
        GameObject newNpc = Instantiate(NPC, Position.position, Quaternion.identity);
        Transform mainObject = newNpc.transform.Find("MainSprite");

        // 메뉴 스프라이트 변경
        SpriteRenderer wantedMenuSprite = mainObject.transform.Find("MenuSprite").GetComponent<SpriteRenderer>();

        // NPC 내에 엤는 NPCController를 찾아서 넣는다
        NPCController controller = newNpc.transform.Find("MainSprite").GetComponent<NPCController>();

        // NPC 메뉴를 정하기 위해서 
        NPCInteraction Interaction = newNpc.transform.Find("MainSprite").GetComponent<NPCInteraction>();

        // 메뉴를 정하는 랜덤 값들을 int 변수에 삽입 -> 음식60%, 맥주20%, 와인 10%, 위스키 10%의 확률로 나올 메서드가 필요
        Menu NPCMenu = (Menu)(UnityEngine.Random.Range(1, Enum.GetNames(typeof(Menu)).Length));
        int MenuIndex = (int)NPCMenu;

        // NPC가 원하는 메뉴의 enum을 선언 한다.
        Interaction.wantedMenu = NPCMenu;

        wantedMenuSprite.sprite = menuSprite[MenuIndex];

        controller.seatTarget = EntranceTargetObject;

        // NPC의 타겟 정보 및 파괴 위치, 그리고 해당 자리의 인덱스를 넣는다.
        controller.SetTarget(randomTarget.transform);

        // 자가 파괴 상태를 위해 위치 게임 오브젝트를 집어 넣는다.
        controller.DestroyTarget = selfDestroyPositionP;

        // 배정 받은 자리의 인덱스를 집어넣는다
        controller.DeployIndex = randomIndex;

        // 딕셔너리의 인덱스와 같은 해당 키 인덱스의 값을 false로 바꾼다
        EmptySeatCheck[randomIndex] = true;

    }

    // 딕셔너리의 false를 찾아서 해당 인덱스를 반환하는 메서드(빈자리를 false, 자리 있음을 true로 판단하는 딕셔너리)
    private int GetRandomTargetIndex()
    {
        // 지역변수
        int randomIdx;

        do
        {
            randomIdx = UnityEngine.Random.Range(0, TargetPrefabList.Count);
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

//private List<int> usedTargetIndex = new List<int>();
//public List<int> UsedTargetIndex { get { return usedTargetIndex; } set { usedTargetIndex = value; } }

//do
//{
//    // 타겟 프리팹의 리스트의 수만큼의 랜덤을 구한다.
//    randomIdx = Random.Range(0, TargetPrefabList.Count);
//}
//while (usedTargetIndex.Contains(randomIdx));    // 사용되었던 프리팹의 인덱스 리스트 안에 같은 숫자가 있으면
//                      // do while 문은 do 문을 실행시키고 나서 while 조건문이 참이면 계속 실행한다.

////사용을 배정받은 인덱스를 더한다.
//usedTargetIndex.Add(randomIndex);