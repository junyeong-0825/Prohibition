#region NameSpace
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
#endregion


public class DayController : MonoBehaviour
{
    #region Fields
    bool IsDay;
    bool IsGameEnd = false;
    [SerializeField] Vector3 dayPosition, nightPosition, dayCameraPosition, nightCameraPosition;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject playerInfoPanel,tutorialPanel;
    [SerializeField] GameObject endStore;
    [SerializeField] Button endBusinessButton;
    [SerializeField] Timer timer;
    [SerializeField] NPCSpawner npcSpawner;
    [SerializeField] PlayerStatus playerStatus;
    #endregion

    void Start()
    {
        IsDay = DataManager.instance.nowPlayer.Playerinfo.IsDay;
        endBusinessButton.onClick.AddListener(()=>{timer.limitTimeSec = 0f; });
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
                endStore.SetActive(true);
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
            endStore.SetActive(false);
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
        playerStatus.ResetStatus();
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
