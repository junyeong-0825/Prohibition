using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스크립터블 오브젝트를 에셋 메뉴에서 생성하기 위한 생성기
// 파일 이름, 메뉴 이름, 항목번호를 지정할 수 있다
[CreateAssetMenu(fileName = "NPC Data", menuName = "Scriptable Object/NPC Data", order = int.MaxValue)]
public class NPCSO : ScriptableObject
{
    // 주문 하고자 하는 음식 매뉴
    [SerializeField] private string orderNormal;
    public string OrderNormal { get { return orderNormal; } }

    // 주문 하고자 하는 밀주 메뉴
    [SerializeField] private string orderBooze;
    public string OrderBooze { get { return orderBooze; } }

    // 움직이고자 하는 타겟 위치
    [SerializeField] private Transform target;
    public Transform Target { get { return target; } }

    // 만족한지 확인
    [SerializeField] private bool isComplicate;
    public bool IsComplicate { get { return IsComplicate; } }

    // 불만족한지 확인
    [SerializeField] private bool isBad;
    public bool IsBad { get { return IsBad; } }

    // 불법인지 확인
    [SerializeField] private bool isIllegal;
    public bool IsIllegal { get { return IsIllegal; } }
}
