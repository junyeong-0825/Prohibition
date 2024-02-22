using UnityEngine;

// 일반 손님의 상호작용 스크립트
public class NPCInteraction : MonoBehaviour
{
    // 메뉴를 요구하는 스프라이트 이미지
    public GameObject OrderMenuSprite;

    // 만족 및 불만족을 표시하는 스프라이트 이미지
    public GameObject satisfiedSprite;
    public GameObject unsatisfiedSprite;

    // 상호작용 제한시간
    public float interactionTimeLimit = 50f;
    // 주문메(위:테스트용, 아래: enum타입)
    public Menu wantedMenu; // 스폰매니저가 스폰 시에 랜덤으로 정해질 것, 타겟 위치랑 파괴 위치 뿌리는 것과 같은 이치

    // 상호작용 상태가 시작되었다는 것을 알리는 bool값
    [SerializeField] private bool interactionStarted = false;
    public bool InteractionStarted { get { return interactionStarted; } }
    // 상호작용이 끝났다는 것을 알리는 bool 값
    [SerializeField] private bool interactionCompleted = false;
    public bool InteractionCompleted { get { return interactionCompleted; } }
    //public bool InteractionCompleted { get { return interactionCompleted; } }
    // 상호작용 시간 초기값
    private float interactionTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        // 상호작용 시작되었고 상호작용 완료값이 false이면
        if(interactionStarted && !interactionCompleted)
        {
            OrderMenuSprite.SetActive(true);
            interactionTimer += Time.deltaTime;
            //Debug.Log(interactionTimer);
            if (interactionTimer >= interactionTimeLimit)
            {
                GameEvents.NotifyTimeOverTrade();
                HandleInteractionFailed();
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
            //Debug.Log("StartInteraction");
            //StartInteraction();
        }
    }

    // 상호작용의 시작 상태를 바꿔주는 메서드
    private void StartInteraction()
    {
        interactionStarted = true;
        //Debug.Log("Interactionstart");
    }

    // enum 비교용 메뉴 비교 메서드
    public void CompareMenu(Menu playerMenu)
    {
        if(!interactionCompleted)
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
        interactionCompleted = true;
        unsatisfiedSprite.SetActive(true);
        InteractionCompleteTargeting();
        //SelfDestroyTargeting();
    }

    // 메뉴 일치 시의 만족 메소드
    private void HandleInteractionSuccess()
    {
        OrderMenuSprite.SetActive(false);
        interactionCompleted = true;
        satisfiedSprite.SetActive(true);
        InteractionCompleteTargeting();
        //SelfDestroyTargeting();
    }

    // 자가 파괴 지정
    private void SelfDestroyTargeting()
    {
        if(interactionCompleted)
        {
            NPCController controller = GetComponent<NPCController>();

            controller.SetTarget(controller.DestroyTarget);
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


    // 테스트용 플레이어 메뉴 비교 메서드
    //public void DeliverMenu(string deliveredMenu)
    //{
    //    if(!interactionCompleted)
    //    {
    //        Debug.Log(deliveredMenu);
    //        //Debug.Log(orderMenu);
    //        if(deliveredMenu == orderMenu)
    //        {
    //            // 상호작용 성공시에 음식값을 전달하는 메서드 실행
    //            OrderMenuSprite.SetActive(false);
    //            interactionCompleted = true;
    //            satisfiedSprite.SetActive(true);
    //            SelfDestroyTargeting();
    //        }
    //        else
    //        {
    //            HandleInteractionFailed();
    //        }
    //    }
    //}
}
