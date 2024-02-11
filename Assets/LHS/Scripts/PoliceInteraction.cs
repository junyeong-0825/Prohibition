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

    private bool CheckingComplete = false;
    private bool CheckingStarted = false;

    // 경찰이 가게 내부에 대기하는데에 걸리는 시간
    public float CheckingTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckingStarted && CheckingTime != 0f)
        {
            // 입구에 들어왔다면 가게 내부를 점검하는 메서드 실행
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

    private void SeachingStart()
    {
        CheckingStarted = true;
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
