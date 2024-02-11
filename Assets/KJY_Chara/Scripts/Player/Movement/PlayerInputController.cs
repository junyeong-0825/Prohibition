using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : CharaController
{
    private Camera _camera;
    private NPCInteraction currentNPCInteraction;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    // 손님 NPC와 콜라이더 트리거 진입시에 손님의 상호작용 컴포넌트를 가져온다
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Guest"))
        {
            currentNPCInteraction = other.GetComponent<NPCInteraction>();
        }
    }

    // 손님 콜라이더 트리거에서 나갈 시 해당 컴포넌트 값은 삭제된다.
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Guest"))
        {
            currentNPCInteraction = null;
        }
    }

    // 상호작용 'E'키를 누를 시에 해당 상호작용 발동
    public void OnInteraction(InputValue value)
    {
        if (currentNPCInteraction != null && value.isPressed && currentNPCInteraction.InteractionStarted)
        {
            Debug.Log("interaction is success");
            DeliverMenuToGuest();
        }
    }

    // 플레이어 메뉴랑 손님 메뉴랑 비교시키는 메서드로 이를 통해서 재화를 얻거나 패널티를 주도록 한다.(테스트용)
    private void DeliverMenuToGuest()
    {
        if(currentNPCInteraction != null && currentNPCInteraction.InteractionStarted)
        {
            string deliverMenu = "test";

            currentNPCInteraction.DeliverMenu(deliverMenu);
            Debug.Log(currentNPCInteraction.orderMenu);
        }
    }

    // 위의 메서드와 같은 용도의 비교용으로써 enum 값으로 비교하며, 실질적으로 패널티 부여나 아니면 재화 획득들 넣을 메소드이다.
    private void DeliverEnumMenu()
    {
        if (currentNPCInteraction != null && currentNPCInteraction.InteractionStarted)
        {
            // 현재 플레이어의 소지 메뉴
            //Menu playerMenu = 0;
        }
    }
}
