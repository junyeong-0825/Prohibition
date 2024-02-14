using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 제한시간 기능을 테스트 하는 중
public class Timer : MonoBehaviour
{
    public float limitTimeSec;
    private float LimitTimeSec { get { return limitTimeSec; } set { limitTimeSec = value; } }
    private int Min;
    private int Sec;
    [SerializeField] private TextMeshProUGUI text_Timer;
    public GameObject[] CheckNPC;

    void Update()
    {
        limitTimeSec -= Time.deltaTime;
        if (limitTimeSec > 0)
        {
            SetTime(limitTimeSec);
            text_Timer.text = string.Format("{0:D2}:{1:D2}", Min, Sec);
        }
        else
        {
            text_Timer.text = "마감시간";
            //Time.timeScale = 0.0f;
            Check();
        }
    }

    /*private void FixedUpdate()
    {

        if (limitTimeSec < 0)
        {
            Check();
        }
    }*/

    private void SetTime(float Sec)
    {
        Min = (int)Sec / 60;
        this.Sec = (int)Sec % 60;
    }

    private void Check()
    {
        CheckNPC = GameObject.FindGameObjectsWithTag("Guest");
    }
}
