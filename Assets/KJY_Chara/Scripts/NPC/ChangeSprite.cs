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
                Debug.Log("음식을 두었다");
                break;
            case Menu.Beer:
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Beer");
                Debug.Log("맥주를 두었다");
                break;
            case Menu.Wine:
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Wine");
                Debug.Log("와인을 두었다");
                break;
            case Menu.Whisky:
                spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Whisky");
                Debug.Log("위스키를 두었다");
                break;
        }
    }
    public void CheckTablePoint()
    {
        spriteRenderer = findTable.dishPoint.GetComponent<SpriteRenderer>();
    }
}
