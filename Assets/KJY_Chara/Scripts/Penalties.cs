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

    // 일반 손님과 술 손님한테 잘못된 음식을 전달 시에
    public void LowLevelGoldPenalty()
    {
        DataManager.instance.nowPlayer.Playerinfo.Gold += 5;
        Inventory.Instance.UpdateUI();
        Debug.Log("물건 값은 5골드 고정");
    }

    // 경찰이 위장 상태 유무에 따라서 벌금을 부여, 일반 장사 패널티가 부여된다면 스택을 쌓는 것이 필요 최대 3번, 3번이 다 쌓이게 된다면 아래 장사 접는 패널티를 부여함
    public void HighLevelGoldPenalty()
    {
        //범칙금으로 돈 뜯어감
        DataManager.instance.nowPlayer.Playerinfo.Gold = DataManager.instance.nowPlayer.Playerinfo.Gold - (DataManager.instance.nowPlayer.Playerinfo.Gold / 5);
        Inventory.Instance.UpdateUI();
        Debug.Log("금주법으로 인한 벌금!");
    }

    // 일반 손님 및 술 손님한테 상호작용을 안할 시에
    public void LowLevelTimePenalty()
    {
        timer.limitTimeSec -= 5f;
        Debug.Log("시간 5초 감소");
    }

    // 누적된 패널티(일반 패널티 누적 총 3회)로 인해 높아진 패널티 -> 패널티 누적은 게임 매니저가 해야 할 것 같다
    public void HighLevelTimePenalty()
    {
        timer.limitTimeSec = 0f;
        Debug.Log("오늘 장사는 접어야돼");
    }
}
