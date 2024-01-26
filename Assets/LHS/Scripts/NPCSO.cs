using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스크립터블 오브젝트를 에셋 메뉴에서 생성하기 위한 생성기
// 파일 이름, 메뉴 이름, 항목번호를 지정할 수 있다
[CreateAssetMenu(fileName = "NPC Data", menuName = "Scriptable Object/NPC Data", order = int.MaxValue)]
public class NPCSO : ScriptableObject
{
    [SerializeField] private string orderNormal;
    public string OrderNormal { get { return orderNormal; } }

    [SerializeField] private string orderBooze;
    public string OrderBooze { get { return orderBooze; } }

    [SerializeField] private string normalMenu;
    public string NormalMenu { get { return normalMenu; } }

    [SerializeField] private string boozeMenu ;
    public string BoozeMenu { get { return boozeMenu; } }

    [SerializeField] private Transform target;
    public Transform Target { get { return target; } }

    [SerializeField] private bool isComplicate;
    public bool IsComplicate { get { return IsComplicate; } }

    [SerializeField] private bool isBad;
    public bool IsBad { get { return IsBad; } }
}
