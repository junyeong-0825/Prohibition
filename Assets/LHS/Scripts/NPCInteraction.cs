using System.Collections;
using System.Collections.Generic;
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

    // 상호작용 실패에 따른 패널티를 호출하는 게임 오브젝트ㄴ
    private Penalties NPCPanel;

    private void Start()
    {
        NPCPanel = GameObject.Find("GameManager").GetComponent<Penalties>();
    }

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
                NPCPanel.LowLevelTimePenalty();
                HandleInteractionFailed();
            }
        }
    }

    // 빈자리 도착시에 상호작용시작 bool 값 변경
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EmptyChair") && !interactionStarted && !interactionCompleted)
        {
            //Debug.Log("StartInteraction");
            StartInteraction();
        }
    }

    private void StartInteraction()
    {
        interactionStarted = true;
        Debug.Log("Interactionstart");
    }

    // enum 비교용 메뉴 비교 메서드
    public void CompareMenu(Menu playerMenu)
    {
        if(!interactionCompleted)
        {
            if(playerMenu == wantedMenu)
            {
                // 해당 메뉴에 맞는 재회를 얻는 메서드가 필요함
                SuccesTrade(playerMenu);

                // 상호작용 완료를 위한 말풍선 바꿈
                OrderMenuSprite.SetActive(false);
                interactionCompleted = true;
                satisfiedSprite.SetActive(true);
                SelfDestroyTargeting();
            }
            else
            {
                NPCPanel.LowLevelGoldPenalty();
                HandleInteractionFailed();
            }

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

    // 메뉴 불일치 시의 불만족 메소드
    private void HandleInteractionFailed()
    {
        OrderMenuSprite.SetActive(false);
        interactionCompleted = true;
        unsatisfiedSprite.SetActive(true);
        SelfDestroyTargeting();
    }

    // 메뉴 일치 시의 만족 메소드
    private void HandleInteractionSuccess()
    {
        OrderMenuSprite.SetActive(false);
        interactionCompleted = true;
        satisfiedSprite.SetActive(true);
        SelfDestroyTargeting();
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

    private void SuccesTrade(Menu playerMenu)
    {
        Item servingItem = DataManager.instance.nowPlayer.items.Find(item => item.Name == playerMenu.ToString());
        //돈을 더해줌
        DataManager.instance.nowPlayer.Playerinfo.Gold += servingItem.SellingPrice;
    }
}
