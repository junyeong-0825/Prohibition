using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 일반 손님의 상호작용 스크립트
public class NPCInteraction : MonoBehaviour
{
    // 만족 및 불만족을 표시하는 스프라이트 이미지
    public GameObject satisfiedSprite;
    public GameObject unsatisfiedSprite;

    // 상호작용 제한시간
    public float interactionTimeLimit = 10f;
    // 주문메(위:테스트용, 아래: enum타입)
    public string orderMenu;
    public Menu wantedMenu; // 스폰매니저가 랜덤으로 조정할 예정

    // 상호작용 상태가 시작되었다는 것을 알리는 bool값
    [SerializeField] private bool interactionStarted = false;
    public bool InteractionStarted { get { return interactionStarted; } }
    // 상호작용이 끝났다는 것을 알리는 bool 값
    public bool interactionCompleted = false;
    //public bool InteractionCompleted { get { return interactionCompleted; } }
    // 상호작용 시간 초기값
    private float interactionTimer = 0f;

    private void Start()
    {
        orderMenu = "test";
        wantedMenu = Menu.Food;
    }

    // Update is called once per frame
    void Update()
    {
        // 상호작용 시작되었고 상호작용 완료값이 false이면
        if(interactionStarted && !interactionCompleted)
        {
            
            interactionTimer += Time.deltaTime;
            //Debug.Log(interactionTimer);
            if (interactionTimer >= interactionTimeLimit)
            {
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("EmptyChair") && interactionStarted && !interactionCompleted)
        {
            //StartInteraction();
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
                interactionCompleted = true;
                satisfiedSprite.SetActive(true);
                SelfDestroyTargeting();
            }
            else
            {
                HandleInteractionFailed();
            }
        }
    }

    // 테스트용 플레이어 메뉴 비교 메서드
    public void DeliverMenu(string deliveredMenu)
    {
        if(!interactionCompleted)
        {
            Debug.Log(deliveredMenu);
            //Debug.Log(orderMenu);
            if(deliveredMenu == orderMenu)
            {
                interactionCompleted = true;
                satisfiedSprite.SetActive(true);
                SelfDestroyTargeting();
            }
            else
            {
                HandleInteractionFailed();
            }
        }
    }

    // 메뉴 불일치 시의 불만족 메소드
    private void HandleInteractionFailed()
    {
        interactionCompleted = true;
        unsatisfiedSprite.SetActive(true);
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
}
