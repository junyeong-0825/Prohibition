#region NameSpace
using System.Collections;
using UnityEngine;
#endregion


public class DayController : MonoBehaviour
{
    #region Fields
    bool IsDay;
    bool IsGameEnd = false;
    [SerializeField] Vector3 dayPosition, nightPosition, dayCameraPosition, nightCameraPosition;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject playerInfoPanel;
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] Timer timer;
    [SerializeField] NPCSpawner npcSpawner;
    [SerializeField] PlayerStatus playerStatus;
    #endregion

    void Start()
    {
        IsDay = DataManager.instance.nowPlayer.Playerinfo.IsDay;
        StartCoroutine("OneDay");
    }

    #region Event
    private void OnEnable()
    {
        GameEvents.OnDayEnd += HandleIsday;
        GameEvents.OnDayStart += IsDayChange;
    }
    private void OnDisable()
    {
        GameEvents.OnDayEnd -= HandleIsday;
        GameEvents.OnDayStart -= IsDayChange;
    }
    #region IsDay Change
    private void HandleIsday()
    {
        IsDay = false;
    }

    void IsDayChange()
    {
        IsDay = true;
    }
    #endregion
    #endregion

    #region OneDay Coroutine
    IEnumerator OneDay()
    {
        while (!IsGameEnd) // IsGameEnd가 false인 동안 루프로 낮과 밤 사이클 반복
        {
            Tutorial();
            if (IsDay)
            {
                #region Day
                SetIsDay();
                playerInfoPanel.SetActive(true);
                SaveData();
                DayTransform();
                DayAudio();
                DayNPC();
                #endregion
            }

            yield return new WaitUntil(() => !IsDay);

            #region Night
            SetIsDay();
            playerInfoPanel.SetActive(false);
            ResetPlayerStatus();
            SaveData();
            NightTransform();
            NightAudio();
            NightNPC();
            #endregion

            yield return new WaitUntil(() => IsDay);
            ClearCheck();
            if (!IsGameEnd) { AddDayCount(); }
        }
    }
    #endregion

    #region Reset Voids
    #region Tutorial Panel
    void Tutorial()
    {
        if(!DataManager.instance.nowPlayer.Playerinfo.IsTutorialed)
        {
            tutorialPanel.SetActive(true);
        }
        else
        {
            tutorialPanel.SetActive(false) ;
        }
    }
    #endregion

    #region Reset Transform
    void DayTransform()
    {
        Player.transform.position = dayPosition;
        mainCamera.transform.position = dayCameraPosition;
    }

    void NightTransform()
    {
        Player.transform.position = nightPosition;
        mainCamera.transform.position = nightCameraPosition;
    }    
    
    #endregion

    #region Reset Audio
    void DayAudio()
    {
        AudioManager.audioInstance.StopPlayNightSound();
        AudioManager.audioInstance.PlayMainSound();
    }

    void NightAudio()
    {
        AudioManager.audioInstance.StartPlayNightSound();
    }
    #endregion

    #region Reset NPC
    void DayNPC()
    {
        timer.limitTimeSec = 240f;
        StartCoroutine(npcSpawner.spawnNPC());
    }
    void NightNPC()
    {
        timer.limitTimeSec = 0f;
        StopCoroutine(npcSpawner.spawnNPC());
    }
    #endregion

    #region Set Days
    void AddDayCount()
    {
        if (DataManager.instance.nowPlayer.Playerinfo.Day < 28)
        {
            DataManager.instance.nowPlayer.Playerinfo.Day++;
        }
        else
        {
            GameEvents.NotifyGameOver();
            IsGameEnd = true;
        }
    }
    void SetIsDay()
    {
        DataManager.instance.nowPlayer.Playerinfo.IsDay = IsDay;
    }
    void SaveData()
    {
        DataManager.instance.SaveAllData();
    }
    #endregion

    #region Reset PlayerStatus
    
    void ResetPlayerStatus()
    {
        if(playerStatus.whatServed != Menu.None)
        {
            PlayerInventory existingItem = DataManager.instance.nowPlayer.inventory.Find(invItem => invItem.Name == playerStatus.whatServed.ToString());
            if (existingItem != null && existingItem.Quantity >= 1)
            {
                existingItem.Quantity++;
                Inventory.Instance.UpdateUI();
            }
            else if (existingItem == null)
            {
                Item item = DataManager.instance.nowPlayer.items.Find(item => item.Name == playerStatus.whatServed.ToString()); 
                PlayerInventory newInventoryItem = new PlayerInventory
                {
                    Classification = item.Classification,
                    Name = item.Name,
                    Quantity = 1,
                    PurchasePrice = item.PurchasePrice,
                    SellingPrice = item.SellingPrice,
                    RiseScale = item.RiseScale,
                    spritePath = "Sprites/" + item.Name,
                    EnhancementValue = 0
                };
                DataManager.instance.nowPlayer.inventory.Add(newInventoryItem);
                Inventory.Instance.UpdateUI();
            }
            playerStatus.whatServed = Menu.None;
            playerStatus.imageSprite.sprite = null;
        }
    }
    #endregion

    #region Clear Check

    void ClearCheck()
    {
        if(DataManager.instance.nowPlayer.Playerinfo.Debt <= 0)
        {
            GameEvents.NotifyGameClear();
            IsGameEnd = true;
        }
    }

    #endregion

    #endregion
}
