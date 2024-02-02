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
            float timeRemaining = 240 - timeElapsed; // 4�п��� ��� �ð��� ��

            // ���� �ð��� ��:�� �������� ǥ��
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
        while (true) // ���� ������ ���� �� ����Ŭ �ݺ�
        {
            // ��
            /*
            ���� ������Ʈ �� Ȱ��ȭ
            NPC Spawner Ȱ��ȭ
            Player Ȱ��ȭ �Ǵ� Player��ġ �̵�
            �� ���� ����
            �� ��� �ʱ�ȭ
            */
            buttonClicked = false;
            mainCamera.transform.position = new Vector3(50, 0, mainCamera.transform.position.z);
            startTime = Time.time;
            isDayTime = true;
            yield return new WaitForSeconds(240f);

            // ��
            /*
            �� ������Ʈ �� Ȱ��ȭ
            Player Ȱ��ȭ �Ǵ� Player��ġ �̵�
            �� ���� ����
            �� ��� �ʱ�ȭ
            */
            timerText.text = "";
            mainCamera.transform.position = new Vector3(100, 0, mainCamera.transform.position.z);
            isDayTime = false;
            yield return new WaitUntil(() => buttonClicked);
        }
    }
}
