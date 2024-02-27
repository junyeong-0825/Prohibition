using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public NPCFindTable findTable;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        findTable = GetComponent<NPCFindTable>();
    }

    public void SettingFood()
    {
        switch (PlayerStatus.instance.whatServed)
        {
            case Menu.None:
                spriteRenderer.sprite = null;
                break;
            case Menu.Food:
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Food");
                break;
            case Menu.Beer:
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Beer");
                break;
            case Menu.Wine:
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Wine");
                break;
            case Menu.Whisky:
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Whisky");
                break;
        }

    }
    public void CheckTablePoint()
    {
        spriteRenderer = findTable.dishPoint.GetComponent<SpriteRenderer>();
    }

    public void CleanTable()
    {
        spriteRenderer.sprite = null;
    }
}
