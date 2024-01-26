using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NPC 타입을 열거형으로 정의함
public enum NPCType
{
    PassNPC,
    CarNPC,
    VisitNormalNPC,
    VisitBoozeNPC,
    VisitPolice,
}

public class NPCInfo : MonoBehaviour
{
    public NPCType nType;

    [SerializeField]
    private Transform Target;
    private GameObject SpawnPosition;
    private GameObject SelfDestroyObject;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetNPCTarget()
    {
        switch (nType)
        {
            case NPCType.PassNPC:
                Target = SpawnPosition.transform;
                break;
            case NPCType.CarNPC:
                break;
            case NPCType.VisitNormalNPC:
                break;
            case NPCType.VisitBoozeNPC:
                break;
            default:
                break;

        }
    }
    
}
