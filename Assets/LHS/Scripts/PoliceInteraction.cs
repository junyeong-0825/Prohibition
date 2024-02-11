using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 경찰NPC를 위한 상호작용 스크립트
public class PoliceInteraction : MonoBehaviour
{
    // 경찰이 식당 내부에 들어왔을 때 나타나는 감정표현 스프라이트
    public GameObject SearchingSprite;

    // 경찰이 가게 내부가 위장되지 않았을 때를 체크하는 bool 값
    public bool IsCheck = true;

    // 경찰 프리팹이 가게 점검을 실]
    private bool CheckingComplete = false;
    private bool CheckingStarted = false;

    // 경찰이 가게 내부에 대기하는데에 걸리는 시간
    public float CheckingTime = 5f;

    // Update is called once per frame
    void Update()
    {
        if(CheckingStarted && !CheckingComplete)
        {
            // 입구에 들어왔다면 가게 내부를 점검하는 메서드 실행
            if(CheckingTime > 0f)
            {
                // 시간 제한을 5초로 두고 0이 될때까지 메서드 실행
                CheckingTime -= Time.deltaTime;
                CheckingRestaurant();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 식당 내부의 입구 쪽 콜라이더(추가예정)에 대해
        if (other.CompareTag("Entrance") && !CheckingStarted && !CheckingComplete)
        {
            //Debug.Log("StartInteraction");
            SeachingStart();
        }
    }

    // 서칭 상태를 시작하는 bool 값 시작
    private void SeachingStart()
    {
        CheckingStarted = true;
    }

    // 서칭 상태를 종료하는 bool 값들 수정 및 타겟 조정
    private void SearchingComplete()
    {
        CheckingComplete = true;
        CheckingStarted = false;
        SelfDestroyTargeting();
    }

    private void CheckingRestaurant()
    {
        // 레스토랑이 위장 상태가 아니면 플레이어한테 패널티 부여 및 스테이지 종료
    }

    // 자가 파괴 지정
    private void SelfDestroyTargeting()
    {
        if (CheckingComplete)
        {
            NPCController controller = GetComponent<NPCController>();

            controller.SetTarget(controller.DestroyTarget);
        }
    }
}
