using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penalties : MonoBehaviour
{
    private Timer timer;
    private void Awake()
    {
        timer = GameObject.Find("UIManager").GetComponent<Timer>();
    }
    public void LowLevelGoldPenalty()
    {
        DataManager.instance.nowPlayer.Playerinfo.Gold += 5;
        Inventory.Instance.UpdateUI();
        Debug.Log("물건 값은 5골드 고정");
    }
    public void HighLevelGoldPenalty()
    {
        //범칙금으로 돈 뜯어감
        DataManager.instance.nowPlayer.Playerinfo.Gold = DataManager.instance.nowPlayer.Playerinfo.Gold - (DataManager.instance.nowPlayer.Playerinfo.Gold / 5);
        Inventory.Instance.UpdateUI();
        Debug.Log("금주법으로 인한 벌금!");
    }
    public void LowLevelTimePenalty()
    {
        timer.limitTimeSec -= 5f;
        Debug.Log("시간 5초 감소");
    }
    public void HighLevelTimePenalty()
    {
        timer.limitTimeSec = 0f;
        Debug.Log("오늘 장사는 접어야돼");
    }
}
