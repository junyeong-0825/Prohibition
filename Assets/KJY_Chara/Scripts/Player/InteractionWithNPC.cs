using System.Xml.Serialization;
using UnityEngine;

public class InteractionWithNPC : MonoBehaviour
{
    [SerializeField] Penalties penalties;
    [SerializeField] PlayerStatus status;
    #region EventAdd
    private void OnEnable()
    {
        GameEvents.OnSuccessTrade += TradeSucceeded;
        GameEvents.OnFailTrade += TradeFailed;
        GameEvents.OnTimeOverTrade += TimeOver;
        GameEvents.OnPolicePenalty += PolicePenalty;
    }
    private void OnDisable()
    {
        GameEvents.OnSuccessTrade -= TradeSucceeded;
        GameEvents.OnFailTrade -= TradeFailed;
        GameEvents.OnTimeOverTrade -= TimeOver;
    }
    #endregion

    void TimeOver()
    {
        penalties.LowLevelTimePenalty();
    }

    void TradeSucceeded()
    {
        AddGold();
        ChangeStatus();
    }

    void TradeFailed()
    {
        penalties.LowLevelGoldPenalty();
        ChangeStatus();
    }
    void AddGold()
    {
        Item TradeItem = DataManager.instance.nowPlayer.items.Find(item => item.Name == status.whatServed.ToString());
        DataManager.instance.nowPlayer.Playerinfo.Gold += TradeItem.SellingPrice;
        Inventory.Instance.UpdateUI();
    }
    void ChangeStatus()
    {
        status.whatServed = Menu.None;
        status.imageSprite.color = new Color(1,1,1,0);
        status.imageSprite.sprite = null;
    }

    void PolicePenalty()
    {
        penalties.HighLevelGoldPenalty();
    }
}
