using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    [SerializeField] GameObject[] MissionButtons;
    int MissionCount;
    bool GameEnd;

    private void Start()
    {
        foreach(var button in MissionButtons) { button.SetActive(false); }
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
    IEnumerator ChangeMission()
    {
        
        if (DataManager.instance.nowPlayer.Playerinfo.Day % 7 == 1)
        {
            MissionButtons[MissionCount].SetActive(true);
            MissionCount++;

            yield return null;
        }
    }

}
