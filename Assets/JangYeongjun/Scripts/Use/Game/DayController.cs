using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DayController : MonoBehaviour
{
    bool dayEnded = false;
    [SerializeField] Vector3 dayPosition;
    [SerializeField] Vector3 nightPosition;
    [SerializeField] Vector3 dayCameraPosition;
    [SerializeField] Vector3 nightCameraPosition;
    [SerializeField] Camera mainCamera;
    [SerializeField] Button dayChangeButton;
    [SerializeField] GameObject Player;
    Timer timer;
    NPCSpawner npcSpawner;
    private void Start()
    {
        npcSpawner = GameObject.Find("NPCSpawner").GetComponent<NPCSpawner>();
        timer = GameObject.Find("UIManager").GetComponent<Timer>();
        GameEvents.OnDayEnd += HandleDayEnd;
        dayChangeButton.onClick.AddListener(OnButtonClick);
        StartCoroutine("OneDay");
        
        //startTime = Time.time;
    }
    private void OnDisable()
    {
        GameEvents.OnDayEnd -= HandleDayEnd;
    }
    private void HandleDayEnd()
    {
        dayEnded = true;
    }

    void OnButtonClick()
    {
        dayEnded = false;
    }

    IEnumerator OneDay()
    {
        Debug.Log("OneDay시작");
        while (true) // 무한 루프로 낮과 밤 사이클 반복
        {
            Debug.Log("낮");
            /*
            밤의 오브젝트 비 활성화
            NPC Spawner 활성화
            낮 장면 초기화
            */
            timer.limitTimeSec = 240;
            StartCoroutine(npcSpawner.spawnNPC());
            Player.transform.position = dayPosition;
            AudioManager.audioInstance.StopPlayNightSound();
            AudioManager.audioInstance.PlayMainSound();
            mainCamera.transform.position = dayCameraPosition;
            yield return new WaitUntil(() => dayEnded);

            Debug.Log("밤");
            /*
            낮 오브젝트 비 활성화
            밤 장면 초기화
            */
            timer.limitTimeSec = 0;
            StopCoroutine(npcSpawner.spawnNPC());
            Player.transform.position = nightPosition;
            AudioManager.audioInstance.StartPlayNightSound();
            mainCamera.transform.position = nightCameraPosition;
            yield return new WaitUntil(() => !dayEnded);
        }
    }
}
