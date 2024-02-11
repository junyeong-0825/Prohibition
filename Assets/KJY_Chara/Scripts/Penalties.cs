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
        TemporaryDataManager.instance.nowPlayer.Playerinfo.Gold += 5;
        Inventory.Instance.UpdateUI();
        Debug.Log("���� ���� 5��� ����");
    }
    public void HighLevelGoldPenalty()
    {
        //��Ģ������ �� ��
        TemporaryDataManager.instance.nowPlayer.Playerinfo.Gold = TemporaryDataManager.instance.nowPlayer.Playerinfo.Gold - (TemporaryDataManager.instance.nowPlayer.Playerinfo.Gold / 5);
        Inventory.Instance.UpdateUI();
        Debug.Log("���ֹ����� ���� ����!");
    }
    public void LowLevelTimePenalty()
    {
        timer.limitTimeSec -= 5f;
        Debug.Log("�ð� 5�� ����");
    }
    public void HighLevelTimePenalty()
    {
        timer.limitTimeSec = 0f;
        Debug.Log("���� ���� ����ߵ�");
    }
}
