using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DayManager : MonoBehaviour
{
    float startTime;
    bool isDayTime = true;
    bool buttonClicked = false;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Camera mainCamera;
    [SerializeField] Button dayChangeButton;
    private void Start()
    {
        dayChangeButton.onClick.AddListener(OnButtonClick);
        StartCoroutine("OneDay");
        startTime = Time.time;
    }

    void Update()
    {
        if (isDayTime)
        {
            float timeElapsed = Time.time - startTime;
            float timeRemaining = 240 - timeElapsed; // 4분에서 경과 시간을 뺌

            // 남은 시간을 분:초 형식으로 표시
            string minutes = ((int)timeRemaining / 60).ToString();
            string seconds = (timeRemaining % 60).ToString("f0");
            timerText.text = minutes + ":" + seconds;
        }
    }
    void OnButtonClick()
    {
        buttonClicked = true;
    }
    IEnumerator OneDay()
    {
        while (true) // 무한 루프로 낮과 밤 사이클 반복
        {
            // 낮
            /*
            밤의 오브젝트 비 활성화
            NPC Spawner 활성화
            Player 활성화 또는 Player위치 이동
            낮 음악 실행
            낮 장면 초기화
            */
            buttonClicked = false;
            mainCamera.transform.position = new Vector3(50, 0, mainCamera.transform.position.z);
            startTime = Time.time;
            isDayTime = true;
            yield return new WaitForSeconds(240f);

            // 밤
            /*
            낮 오브젝트 비 활성화
            Player 활성화 또는 Player위치 이동
            밤 음악 실행
            밤 장면 초기화
            */
            timerText.text = "";
            mainCamera.transform.position = new Vector3(100, 0, mainCamera.transform.position.z);
            isDayTime = false;
            yield return new WaitUntil(() => buttonClicked);
        }
    }
}
