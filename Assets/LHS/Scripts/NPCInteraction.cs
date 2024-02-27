using UnityEngine;
using System.Collections;

// 일반 손님의 상호작용 스크립트
public class NPCInteraction : MonoBehaviour
{
    // 메뉴를 요구하는 스프라이트 이미지
    public GameObject OrderMenuSprite;
    public Sprite DefaultMenuSprite;

    // 만족 및 불만족을 표시하는 스프라이트 이미지
    public GameObject satisfiedSprite;
    public GameObject unsatisfiedSprite;
    public GameObject chooseSprite;
    public GameObject SuccessSpriteObject;
    public GameObject FailSpriteObject;
    public GameObject SmokedSpriteobject;

    // 상호작용 제한시간
    public float interactionTimeLimit = 15f;
    // 주문메(위:테스트용, 아래: enum타입)
    public Menu wantedMenu; // 스폰매니저가 스폰 시에 랜덤으로 정해질 것, 타겟 위치랑 파괴 위치 뿌리는 것과 같은 이치

    // 상호작용 상태가 시작되었다는 것을 알리는 bool값
    [SerializeField] private bool interactionStarted = false;
    public bool InteractionStarted { get { return interactionStarted; } }
    // 상호작용이 끝났다는 것을 알리는 bool 값
    [SerializeField] private bool interactionCompleted = false;
    public bool InteractionCompleted { get { return interactionCompleted; } }

    // 위장상태 감지와 경찰 탐문 감지를 위한 변수 선언
    private PlayerStatus playerStatus;
    private bool isUndercover;

    // 상호작용 시간 초기값
    private float interactionTimer = 0f;
    // 위장 상태에 돌입되면 더는 바뀔 수 없는지에 확인해주는 bool값
    private bool isChanged;
    // Update문 내에서 한번 실행만 하도록 하기 위한 플래그 bool값
    private bool isflag = false;

    //김준영의 작업 추가분
    public ChangeSprite changeSprite;
    private void Awake()
    {
        changeSprite = GetComponent<ChangeSprite>();
    }

    private void Start()
    {
        playerStatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
        if (playerStatus == null)
        {
            Debug.LogError("PlayerStatus를 찾을 수 없습니다.");
        }
        StartCoroutine(PlayerUndercoverStatus());
    }

    // Update is called once per frame
    void Update()
    {
        isUndercover = playerStatus.isUndercover;
        // 상호작용 시작되었고 상호작용 완료값이 false이면
        if (interactionStarted && !interactionCompleted)
        {
            OrderMenuSprite.SetActive(true);

            if (isChanged && (int)wantedMenu > 1)
            {
                DodgePoliceSearch();
            }

            // 메뉴가 일반 음식일때는
            else
            {
                interactionTimer += Time.deltaTime;
                TimeLimited();
            }


        }

    }

    // 빈자리 도착시에 상호작용시작 bool 값 변경
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EmptyChair") && !interactionStarted && !interactionCompleted)
        {
            NPCController controller = GetComponent<NPCController>();
            if(controller.seatTarget == other.transform)
            {
                StartInteraction();
            }
        }

        //플레이어가 상호작용이 되는 손님 근처에 있을 때에
        if (other.CompareTag("Player") && interactionStarted)
        {
            chooseSprite.SetActive(true);
        }
    }

    // 플레이어가 손님 주변에서 떠나면 해당 손님에게서 타겟팅을 사라지게 함
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            chooseSprite.SetActive(false);
        }
    }

    // 위장 단속을 피하기 위한 손님의 행동 패턴을 변화시킴
    private void DodgePoliceSearch()
    {
        int randomIndex = Random.Range(0, 3);

        switch(randomIndex)
        {
            // 바로 자리를 뜨도록 하는 행동 패턴
            case 0:
                SuddenInteractionComplete();
                break;

            // 술을 일반 음식으로 바꾸도록 하는 메서드
            case 1:
                ChangeBoozeToFood();
                break;

            case 2:
                TwistedOrder();
                break;
        }
    }

    // 상호작용의 시작 상태를 바꿔주는 메서드
    private void StartInteraction()
    {
        interactionStarted = true;
    }

    // enum 비교용 메뉴 비교 메서드
    public void CompareMenu(Menu playerMenu)
    {
        changeSprite.SettingFood();
        if (!interactionCompleted && interactionStarted)
        {
            if(playerMenu == wantedMenu)
            {
                //상호작용 완료 시 데이터 바꿔줌
                GameEvents.NotifySuccesTrade();
                // 상호작용 완료를 위한 말풍선 바꿈
                HandleInteractionSuccess();
            }
            else
            {
                //상호작용 완료 시 데이터 바꿔줌
                GameEvents.NotifyFailTrade();
                HandleInteractionFailed();
            }
        }
    }

    // 메뉴 불일치 시의 불만족 메소드
    private void HandleInteractionFailed()
    {
        OrderMenuSprite.SetActive(false);
        SmokedSpriteobject.SetActive(false);
        interactionCompleted = true;
        unsatisfiedSprite.SetActive(true);
        InteractionCompleteTargeting();
    }

    // 메뉴 일치 시의 만족 메소드
    private void HandleInteractionSuccess()
    {
        OrderMenuSprite.SetActive(false);
        interactionCompleted = true;
        satisfiedSprite.SetActive(true);
        InteractionCompleteTargeting();
    }

    // (위장상태 진입 및 술손님일 때)
    // 1. 바로 자리를 떠나는 메소드
    private void SuddenInteractionComplete()
    {
        OrderMenuSprite.SetActive(false);
        interactionCompleted = true;
        InteractionCompleteTargeting();
    }

    // 2. 술을 일반 메뉴로 바꿔주고 바로 시간을 초기화하는 메서드
    private void ChangeBoozeToFood()
    {
        if (!isflag)
        {
            wantedMenu = (Menu)1;
            DefaultMenuSprite = GameObject.Find("NPCSpawner").GetComponent<NPCSpawner>().menuSprite[1];
            OrderMenuSprite.GetComponent<SpriteRenderer>().sprite = DefaultMenuSprite;
            interactionTimer = 0f;
            isflag = true;
        }

        if(!interactionCompleted)
        {
            interactionTimer += Time.deltaTime;
            TimeLimited();
        }
    }

    // 3. 시간을 초기화하고 메뉴 이미지를 다른 이미지로 교체해서 음식이 무엇인지 알지 못하게 하기
    private void TwistedOrder()
    {
        OrderMenuSprite.SetActive(false);
        SmokedSpriteobject.SetActive(true);
        if (!isflag)
        {
            interactionTimer = 0f;
            isflag = true;
            isChanged = false;
        }
        if (!interactionCompleted)
        {
            interactionTimer += Time.deltaTime;
            if(interactionTimer > interactionTimeLimit)
            {
                GameEvents.NotifyTimeOverTrade();
                SmokedSpriteobject.SetActive(false);
                interactionCompleted = true;
                unsatisfiedSprite.SetActive(true);
                InteractionCompleteTargeting();
            }
        }
    }

    // 상호작용이 시작되고 나서 일정 시간이 지나면 실패되도록 함
    private void TimeLimited()
    {
        if (interactionTimer >= interactionTimeLimit)
        {
            GameEvents.NotifyTimeOverTrade();
            HandleInteractionFailed();
        }
    }

    // 상호작용이 완료된 후 내부 입구로 가기 위한 타겟팅을 시도한다(이때 nextTarget에 들어가 있는 좌표값은 내부 입구로 설정되어 있다.)
    private void InteractionCompleteTargeting()
    {
        if(interactionCompleted)
        {
            NPCController controller = GetComponent<NPCController>();
            controller.SetTarget(controller.nextTarget);
        }
    }

    // 플레이어의 위장 상태를 감지하는 코루틴 함수 (start() 이벤트 함수에서 실행)
    private IEnumerator PlayerUndercoverStatus()
    {
        yield return new WaitUntil(() => isUndercover == true && interactionStarted);
        isChanged = true;
    }
}
