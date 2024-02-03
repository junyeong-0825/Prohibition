using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DayManager : MonoBehaviour
{
    //float startTime;
    //bool isDayTime = true;
    bool buttonClicked = false;
    bool dayEnded = false;
    //float delayDaySecond = 240f;
    [SerializeField] Vector3 dayPosition;
    [SerializeField] Vector3 nightPosition;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Camera mainCamera;
    [SerializeField] Button dayChangeButton;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject cctvButton;
    [SerializeField] GameObject Player;
    private Timer timer;
    private void Start()
    {
        GameEvents.OnDayEnd += HandleDayEnd;
        dayChangeButton.onClick.AddListener(OnButtonClick);
        StartCoroutine("OneDay");
        //startTime = Time.time;
    }
    private void OnEnable()
    {
        GameEvents.OnDayEnd -= HandleDayEnd;
    }
    private void HandleDayEnd()
    {
        dayEnded = true;
    }
    /*
    void Update()
    {
        if (isDayTime)
        {
            float timeElapsed = Time.time - startTime;
            float timeRemaining = delayDaySecond - timeElapsed; // 4분에서 경과 시간을 뺌

            // 남은 시간을 분:초 형식으로 표시
            string minutes = ((int)timeRemaining / 60).ToString();
            string seconds = (timeRemaining % 60).ToString("f0");
            timerText.text = minutes + ":" + seconds;
        }
    }
    */
    void OnButtonClick()
    {
        buttonClicked = true;
    }
    IEnumerator OneDay()
    {
        Debug.Log("OneDay시작");
        while (true) // 무한 루프로 낮과 밤 사이클 반복
        {
            Debug.Log("낮");
            // 낮
            /*
            밤의 오브젝트 비 활성화
            NPC Spawner 활성화
            낮 음악 실행
            낮 장면 초기화
            */
            //cctvButton.SetActive(true);

            //timer.limitTimeSec = 10f;
            Player.transform.position = dayPosition;
            StopCoroutine(AudioController.audioInstance.PlayNightSound());
            audioSource.Stop();
            AudioController.audioInstance.PlayMainSound();
            buttonClicked = false;
            mainCamera.transform.position = new Vector3(50, 0, mainCamera.transform.position.z);
            //startTime = Time.time;
            //isDayTime = true;
            yield return new WaitUntil(() => dayEnded);

            Debug.Log("밤");
            // 밤
            /*
            낮 오브젝트 비 활성화
            밤 음악 실행
            밤 장면 초기화
            */
            //cctvButton.SetActive(false);

            //timer.limitTimeSec = 0f;
            Player.transform.position = nightPosition;
            audioSource.Stop();
            StartCoroutine(AudioController.audioInstance.PlayNightSound());
            dayEnded = false;
            timerText.text = "";
            mainCamera.transform.position = new Vector3(100, 0, mainCamera.transform.position.z);
            //isDayTime = false;
            yield return new WaitUntil(() => buttonClicked);
        }
    }
    private void SetTime()
    {
        timer = GameObject.Find("UIManager").GetComponent<Timer>();
    }
}
