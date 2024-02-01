using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 제한시간 기능을 테스트 하는 중
public class Timer : MonoBehaviour
{
    [SerializeField] private float limitTimeSec;
    private int Min;
    private int Sec;
    [SerializeField] private TextMeshProUGUI text_Timer;

    void Start()
    {

    }

    void Update()
    {
        limitTimeSec -= Time.deltaTime;
        if (limitTimeSec >= 0)
        {
            SetTime(limitTimeSec);
            text_Timer.text = string.Format("{0:D2}:{1:D2}", Min, Sec);
        }
        else
        {
            text_Timer.text = "마감시간";
            //Time.timeScale = 0.0f;
        }
    }

    private void SetTime(float Sec)
    {
        Min = (int)Sec / 60;
        this.Sec = (int)Sec % 60;
        if(Min > (Sec/60f))
        {
            Min -= 1;
        }
    }
}
