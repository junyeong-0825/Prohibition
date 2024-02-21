#region NameSpace
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
#endregion


public class DayController : MonoBehaviour
{
    #region Fields
    bool IsDay = true;
    [SerializeField] Vector3 dayPosition, nightPosition, dayCameraPosition, nightCameraPosition;
    [SerializeField] Camera mainCamera;
    [SerializeField] Button dayChangeButton;
    [SerializeField] GameObject Player;
    [SerializeField] Timer timer;
    [SerializeField] NPCSpawner npcSpawner;
    #endregion

    private void Start()
    {
        StartCoroutine("OneDay");
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
    #endregion
    #endregion

    #region OneDay Coroutine
    IEnumerator OneDay()
    {
        while (true) // 무한 루프로 낮과 밤 사이클 반복
        {
            SaveData();

            Debug.Log("Day");

            // 낮 장면 초기화
            DayTransform();
            DayAudio();
            DayNPC();

            yield return new WaitUntil(() => !IsDay);

            SaveData();

            Debug.Log("Night");

            // 밤 장면 초기화
            NightTransform();
            NightAudio();
            NightNPC();

            yield return new WaitUntil(() => IsDay);

            Debug.Log("Add days");
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
    }
    void SaveData()
    {
        DataManager.instance.SaveAllData();
    }
    #endregion

    #endregion
}
