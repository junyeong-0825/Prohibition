using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스크립트 정리 대상
public class NPC : MonoBehaviour
{
    [SerializeField] private NPCSO npcSo;
    public NPCSO NpcSo { set { npcSo = value; } }

    public void SetNPCInfo()
    {

    }
}
