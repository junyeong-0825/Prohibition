using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penalties : MonoBehaviour
{
    Timer timer = new Timer();
    public void LowLevelGoldPenalty()
    {
        Inventory.Instance.haveGold += 5;
    }
    public void HighLevelGoldPenalty()
    {
        //¹üÄ¢±ÝÀ¸·Î µ· ¶â¾î°¨
        Inventory.Instance.haveGold = Inventory.Instance.haveGold - (Inventory.Instance.haveGold / 5);
    }
    public void LowLevelTimePenalty()
    {
        timer.limitTimeSec -= 5f;
    }
    public void HighLevelTimePenalty()
    {
        timer.limitTimeSec = 0f;
    }
}
