using System.Xml.Serialization;
using System.Collections;
using TMPro;
using UnityEngine;

public class InteractionWithNPC : MonoBehaviour
{
    int foodGold, beerGold, wineGold, whiskyGold, startGold, endGold, policePenaltyGold, failPenaltyGold;
    [SerializeField] Penalties penalties;
    [SerializeField] PlayerStatus status;
    [SerializeField] AudioSource resultAudioSource;
    [SerializeField] AudioClip resultAudioClip;
    [SerializeField] GameObject resultPanel;
    [SerializeField] GameObject exitButton;
    [SerializeField] TextMeshProUGUI foodText, beerText, wineText, whiskyText, startText, endText, policePenaltyText, failPenaltyText;
    #region EventAdd
    private void OnEnable()
    {
        GameEvents.OnDayStart += ResetPanel;
        GameEvents.OnDayEnd += ResultPanel;
        GameEvents.OnSuccessTrade += TradeSucceeded;
        GameEvents.OnFailTrade += TradeFailed;
        GameEvents.OnTimeOverTrade += TimeOver;
        GameEvents.OnPolicePenalty += PolicePenalty;
    }
    private void OnDisable()
    {
        GameEvents.OnDayStart -= ResetPanel;
        GameEvents.OnDayEnd -= ResultPanel;
        GameEvents.OnSuccessTrade -= TradeSucceeded;
        GameEvents.OnFailTrade -= TradeFailed;
        GameEvents.OnTimeOverTrade -= TimeOver;
        GameEvents.OnPolicePenalty -= PolicePenalty;
    }
    #endregion
    private void Start()
    {
        startGold = DataManager.instance.nowPlayer.Playerinfo.Gold;
    }
    void ResultPanel()
    {
        endGold = DataManager.instance.nowPlayer.Playerinfo.Gold;
        resultPanel.SetActive(true);
        ResultTextSetting();
        StartCoroutine(ResultGoldSetting());
    }
    void ResetPanel()
    {
        startGold = DataManager.instance.nowPlayer.Playerinfo.Gold;
        resultPanel.SetActive(false);
        foodGold = 0;
        beerGold = 0;
        wineGold = 0;
        whiskyGold = 0;
        policePenaltyGold = 0;
        failPenaltyGold = 0;
    }
    void TimeOver()
    {
        penalties.LowLevelTimePenalty();
    }

    void TradeSucceeded()
    {
        Item TradeItem = DataManager.instance.nowPlayer.items.Find(item => item.Name == status.whatServed.ToString());
        AddGold(TradeItem);
        ResultItemGold(TradeItem);
        ChangeStatus();
    }

    void TradeFailed()
    {
        penalties.LowLevelGoldPenalty();
        failPenaltyGold += 5;
        ChangeStatus();
    }
    void AddGold(Item item)
    {
        DataManager.instance.nowPlayer.Playerinfo.Gold += item.SellingPrice;
        Inventory.Instance.UpdateUI();
    }
    void ChangeStatus()
    {
        status.whatServed = Menu.None;
        status.imageSprite.sprite = null;
    }
    IEnumerator ResultGoldSetting()
    {
        PlayResultAudio();
        resultAudioSource.Play();
        startText.text = $"={startGold} Gold";
        yield return new WaitForSecondsRealtime(resultAudioSource.clip.length);
        resultAudioSource.Play();
        beerText.text = $"+{beerGold} Gold";
        yield return new WaitForSecondsRealtime(resultAudioSource.clip.length);
        resultAudioSource.Play();
        wineText.text = $"+{wineGold} Gold";
        yield return new WaitForSecondsRealtime(resultAudioSource.clip.length);
        resultAudioSource.Play();
        whiskyText.text = $"+{whiskyGold} Gold";
        yield return new WaitForSecondsRealtime(resultAudioSource.clip.length);
        resultAudioSource.Play();
        foodText.text = $"+{foodGold} Gold";
        yield return new WaitForSecondsRealtime(resultAudioSource.clip.length);
        resultAudioSource.Play();
        failPenaltyText.text = $"+{failPenaltyGold} Gold";
        yield return new WaitForSecondsRealtime(resultAudioSource.clip.length);
        resultAudioSource.Play();
        policePenaltyText.text = $"-{policePenaltyGold} Gold";
        yield return new WaitForSecondsRealtime(resultAudioSource.clip.length);
        resultAudioSource.Play();
        endText.text = $"={endGold} Gold";
        exitButton.SetActive(true);
    }
    void PlayResultAudio()
    {
        resultAudioSource.clip = resultAudioClip;
    }
    void ResultTextSetting()
    {
        resultAudioSource.clip = null;
        resultAudioSource.Stop();
        exitButton.SetActive(false);
        foodText.text = "";
        beerText.text = "";
        wineText.text = "";
        whiskyText.text = "";
        startText.text = "";
        endText.text = "";
        policePenaltyText.text = "";
        failPenaltyText.text = "";
    }

    void ResultItemGold(Item item)
    {
        if(status.whatServed == Menu.Beer) beerGold += item.SellingPrice;
        else if(status.whatServed == Menu.Wine) wineGold += item.SellingPrice;
        else if(status.whatServed == Menu.Whisky) whiskyGold += item.SellingPrice;
        else if(status.whatServed == Menu.Food) foodGold += item.SellingPrice;
    }

    void PolicePenalty()
    {
        int beforeGold = DataManager.instance.nowPlayer.Playerinfo.Gold;
        penalties.HighLevelGoldPenalty();
        policePenaltyGold += beforeGold - (DataManager.instance.nowPlayer.Playerinfo.Gold);
    }
}
