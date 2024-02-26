using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkOnTheTable : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer ImagePoint_Bottom;
    [SerializeField]
    private SpriteRenderer ImagePoint_Top;
    [SerializeField]
    private SpriteRenderer ImagePoint_Right;
    [SerializeField]
    private SpriteRenderer ImagePoint_Left;
    [SerializeField]
    private GameObject SmokeEffect;

    private TableRayCast tableRayCast;
    private bool IsUnderCovered
    {
        get { return PlayerStatus.instance.isUndercover; }
    }
    private void Awake()
    {
        tableRayCast = GetComponent<TableRayCast>();
    }
    
    public void OnSmoke()
    {
        if (IsUnderCovered)
        {
            SmokeEffect.SetActive(true);
        }
        else
        { 
            SmokeEffect.SetActive(false);
        }
    }

    public void ServingFood()
    {
        if (tableRayCast.isChangeBottom)
        {

        }
        
        
    }
}
