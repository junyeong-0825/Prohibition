using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
   
    // 오브젝트를 넣을 프리팹과 스프라이트
    [SerializeField] private GameObject[] guestPrefab;
    public Sprite[] menuSprite;
    [SerializeField] private GameObject policePrefab;

    // 오브젝트 스폰과 파괴 위치값
    [SerializeField] private Transform SpawnPositionPrefab;
    // 식당 입구의 위치값을 가지는 변수값
    [SerializeField] private Transform EntranceTargetObject;

    [SerializeField] private Transform SearchLocationObject;

    [SerializeField] private List<GameObject> TargetPrefabList;
    public Dictionary<int, bool> EmptySeatCheck = new();
    [SerializeField] private Transform selfDestroyPositionP; 

    public bool IsPoliceSpawned = false;

    // 스폰 생성 지연시간 및 NPC 수량 변수 선언
    private float guestInterval = 3.5f;
    private int guestCount;
    private float policeInterval = 1f;
    private int policeCount;
    private List<int> equipedMenu = new List<int>();

    [SerializeField] private Timer timeLeft;

    void Start()
    {
        SetSeatCheck();

    }

    // 일반 NPC를 스폰하는 코루틴 메서드
    internal IEnumerator spawnNPC()
    {
        // 손님의 갯수 초기화
        int callPoliceCount = 0;
        int callGuestCount = 0;

        SetDayWaves();
        CheckInventory();

        //실전용
        while (true)
        {

            // 빈자리 수만큼 NPC가 스폰되었다면 스폰 상태를 체크하는 bool값 선언
            bool isEmpty = AreAllValuesFalse(EmptySeatCheck);

            // 시간이 다 되면 마감시간 상태가 되어 NPC 퇴장이 다 되는지 확인 
            if(timeLeft.limitTimeSec <= 0f)
            {
                yield return new WaitUntil(() => timeLeft.CheckNPC.Length == 0);
                GameEvents.NotifyDayEnd();
                yield break;
            }

            // 빈자리가 있다면 스폰해주는데 조건에 따라서 코루틴들을 실행시키도록 함 
            if (!isEmpty)
            {
                int index = UnityEngine.Random.Range(0, guestPrefab.Length);
                int gacha = UnityEngine.Random.Range(0, 10);
                if (gacha < 2 && callPoliceCount <= policeCount && !IsPoliceSpawned)
                {
                    yield return SpawnPolice(policeInterval, policePrefab, SpawnPositionPrefab);
                    callPoliceCount++;
                    IsPoliceSpawned = true;
                }
                else if( callGuestCount <= guestCount)
                {
                    yield return SpawnOnce(guestInterval, guestPrefab[index], SpawnPositionPrefab);
                    callGuestCount++;
                }
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
        //Debug.Log("Drop Prefab");
        // int 변수 안에는 빈자리 인덱스에 해당하는 변수를 출력해서 넣는다
        int randomIndex = GetRandomTargetIndex(); 

        // 해당 자리인 게임 오브젝트를 변수 안에 집어 넣는다
        GameObject randomTarget = TargetPrefabList[randomIndex];

        // 스폰하고자 하는 프리팹을 게임 상으로 소환시킨다.
        GameObject newNpc = Instantiate(NPC, Position.position, Quaternion.identity);

        // NPC 내에 엤는 NPCController를 찾아서 넣는다
        NPCController controller = newNpc.transform.Find("MainSprite").GetComponent<NPCController>();

        // NPC 메뉴를 정하기 위해서 
        NPCInteraction Interaction = newNpc.transform.Find("MainSprite").GetComponent<NPCInteraction>();

        SetGuestMenu(newNpc, Interaction);

        // 스폰된 NPC의 타겟을 바깥 입구쪽으로 향하게 한다.
        controller.target = EntranceTargetObject;

        // 다음 행동에 필요한 빈좌석 좌표값을 넣는다.
        controller.seatTarget = randomTarget.transform;

        // 자가 파괴 상태를 위해 위치 게임 오브젝트를 집어 넣는다.
        controller.DestroyTarget = selfDestroyPositionP;

        // NPC의 이동할 타겟을 설정한다.
        controller.SetTarget(controller.target);

        // 배정 받은 자리의 인덱스를 집어넣는다
        controller.DeployIndex = randomIndex;

        // 딕셔너리의 인덱스와 같은 해당 키 인덱스의 값을 false로 바꾼다
        EmptySeatCheck[randomIndex] = true;

    }

    private void SetGuestMenu(GameObject npcPrefab, NPCInteraction prefabInteraction)
    {
        // 메뉴 이미지를 받아오기에 필요한 오브젝트들
        Transform mainObject = npcPrefab.transform.Find("MainSprite");
        SpriteRenderer wantedMenuSprite = mainObject.transform.Find("MenuSprite").GetComponent<SpriteRenderer>();

        // 메뉴를 정하는 랜덤 값들을 int 변수에 삽입 -> 음식60%, 맥주20%, 와인 10%, 위스키 10%의 확률로 나올 메서드가 필요
        Menu NPCMenu = (Menu)RandomMenuSelect();
        int MenuIndex = (int)NPCMenu;

        // NPC가 원하는 메뉴의 enum을 선언 한다.
        prefabInteraction.wantedMenu = NPCMenu;

        // 메뉴의 스프라이트 데이터를 집어 넣는다.
        wantedMenuSprite.sprite = menuSprite[MenuIndex];
        
    }

    // 경찰 프리팹을 스폰하는 메서드
    private void SpawnPrefabPolice(GameObject NPC, Transform Position)
    {
        GameObject newNpc = Instantiate(NPC, Position.position, Quaternion.identity);

        NPCController controller = newNpc.transform.Find("MainSprite").GetComponent<NPCController>();

        controller.target = EntranceTargetObject;

        controller.nextTarget = SearchLocationObject;

        controller.DestroyTarget = selfDestroyPositionP;

        controller.SetTarget(controller.target);
    }

    //경찰을 스폰하는 메서드
    //조건 : 제한 시간 내에 스폰 횟수를 충족시켜야 함, 경찰은 한 스테이지 당 총 3번 스폰된다, 스폰 되면 다른 경찰을 스폰할 수 없다,
    private IEnumerator SpawnPolice(float interval, GameObject NPC, Transform Position)
    {

        yield return new WaitForSeconds(interval);
        SpawnPrefabPolice(NPC, Position);
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

    // 메뉴를 확률로 정하게 해주는 메서드 (음식 50, 맥주 30, 와인과 위스키 각각 10 정도로 드랍해준다)
    private int RandomMenuSelect()
    {
        int SelectedMenu;
        SelectedMenu = UnityEngine.Random.Range(0, 10);

        int index;
        index = UnityEngine.Random.Range(0, equipedMenu.Count);

        if (SelectedMenu > 4) SelectedMenu = 1;
        else if (SelectedMenu > 1) SelectedMenu = 2;
        else if (SelectedMenu > 0) SelectedMenu = 3;
        else SelectedMenu = 4;


        return equipedMenu[index];
    }

    private void CheckInventory()
    {
        if (equipedMenu.Count > 0) equipedMenu.Clear();
        PlayerInventory foodInven = DataManager.instance.nowPlayer.inventory.Find(inven => inven.Name == "Food");
        PlayerInventory beerInven = DataManager.instance.nowPlayer.inventory.Find(inven => inven.Name == "Beer");
        PlayerInventory wineInven = DataManager.instance.nowPlayer.inventory.Find(inven => inven.Name == "Wine");
        PlayerInventory whiskyInven = DataManager.instance.nowPlayer.inventory.Find(inven => inven.Name == "Whisky");
        if (foodInven != null) equipedMenu.Add(1);
        if (beerInven != null) equipedMenu.Add(2);
        if (wineInven != null) equipedMenu.Add(3);
        if (whiskyInven != null) equipedMenu.Add(4);
        else equipedMenu.Add(1);
    }

    // 플레이 시작 후 몇 날인지를 파악하여, 손님 수와 경찰 수를 조절하는 메서드
    private void SetDayWaves()
    {
        int Today = DataManager.instance.nowPlayer.Playerinfo.Day;

        if(Today >= 0 && Today < 10)
        {
            guestInterval = 9f;
            guestCount = 30;
            policeInterval = 3f;
            policeCount = 1;
        }
        else if(Today >=10 && Today < 19)
        {
            guestInterval = 7f;
            guestCount = 50;
            policeInterval = 2.5f;
            policeCount = 2;
        }
        else if(Today >= 19 && Today < 29)
        {
            guestInterval = 5f;
            guestCount = 70;
            policeInterval = 2f;
            policeCount = 3;
        }
        else
        {
            guestInterval = 4f;
            guestCount = 90;
            policeInterval = 1.5f;
            policeCount = 7;
        }
    }

    // 딕셔너리의 Value값들이 모두가 true이면 true를 반환하는 메서드, 빈자리를 뜻하는 false 값이 키 값인 각 자리의 상태를 표시하는 딕셔너리를 체크한다.
    private bool AreAllValuesFalse(Dictionary<int, bool> dict)
    {
        foreach(var pair in dict.Values)
        {
            if(!pair)
            {
                return false;
            }
        }
        return true;
    }

    // 빈자리 오브젝트를 담은 리스트들을 검사하여 빈자리를 체크할 수 있는 딕셔너리를 만들어주는 메서드
    private void SetSeatCheck()
    {
        //Debug.Log("함수실행?");
        int Count = 0;
        
        foreach(GameObject Seat in TargetPrefabList)
        {
            EmptySeatCheck.Add(Count, false);
            //Debug.Log(EmptySeatCheck.ContainsKey(Count));
            //Debug.Log(EmptySeatCheck.ContainsValue(false));
            Count++;
        }
    }
    
}



//// 테스트용
//while (true)
//{
//if (usedTargetIndex.Count > TargetPrefabList.Count)
//{
//    Debug.Log("Wait Spawn");
//    yield break;
//}
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