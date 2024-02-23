#region NameSpace
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
#endregion


public class DayController : MonoBehaviour
{
    #region Fields
    bool IsDay = true;
    bool IsGameEnd = false;
    [SerializeField] Vector3 dayPosition, nightPosition, dayCameraPosition, nightCameraPosition;
    [SerializeField] Camera mainCamera;
    [SerializeField] Button dayChangeButton;
    [SerializeField] Button testButton;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject playerInfoPanel;
    [SerializeField] TextMeshProUGUI daysText;
    [SerializeField] Timer timer;
    [SerializeField] NPCSpawner npcSpawner;
    [SerializeField] PlayerStatus playerStatus;
    #endregion

    void Start()
    {
        StartCoroutine("OneDay");
        testButton.onClick.AddListener(TestIsDayChange);
    }

    #region Event
    private void OnEnable()
    {
        GameEvents.OnDayEnd += HandleIsday;
        dayChangeButton.onClick.AddListener(IsDayButtonChange);
    }
    private void OnDisable()
    {
        GameEvents.OnDayEnd -= HandleIsday;
    }
    #region IsDay Change
    private void HandleIsday()
    {
        IsDay = false;
    }

    void IsDayButtonChange()
    {
        IsDay = true;
    }
    void TestIsDayChange()
    {
        if(IsDay)
        {
            IsDay=false;
        }
        else { IsDay=true;}
    }
    #endregion
    #endregion

    #region OneDay Coroutine
    IEnumerator OneDay()
    {
        while (!IsGameEnd) // 무한 루프로 낮과 밤 사이클 반복
        {
            #region Day
            playerInfoPanel.SetActive(true);
            SaveData();
            DayTransform();
            DayAudio();
            DayNPC();
            #endregion

            yield return new WaitUntil(() => !IsDay);

            #region Night
            playerInfoPanel.SetActive(false);
            ResetPlayerStatus();
            SaveData();
            NightTransform();
            NightAudio();
            NightNPC();
            #endregion

            yield return new WaitUntil(() => IsDay);

            AddDayCount();
        }
    }
    #endregion

    #region Reset Voids

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

    #region After Day
    void AddDayCount()
    {
        DataManager.instance.nowPlayer.Playerinfo.Day ++;
        daysText.text = DataManager.instance.nowPlayer.Playerinfo.Day.ToString();
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

    #endregion
}
