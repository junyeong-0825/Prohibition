using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{
    [SerializeField] GameObject MissionOverText;
    [SerializeField] GameObject[] MissionButtons;
    [SerializeField] TextMeshProUGUI nameText, scriptText, descriptionText, rewardText;
    [SerializeField] Button[] missionButtons;
    string[] additionalDescription;
    int lastCheckedDay = 0; // 마지막으로 확인한 날짜 저장
    int foodCount, whiskyCount;
    int weeks;

    private void Start()
    {
        MissionOverText.SetActive(false);
        ResetText();
        additionalDescription = new string[DataManager.instance.nowPlayer.missions.Count];
        // 모든 미션 버튼 비활성화
        foreach (var button in MissionButtons)
        {
            button.SetActive(false);
        }

        UpdateMissionButtons(); // 게임 시작 시 미션 버튼 상태 업데이트


        for(int i = 0; i < missionButtons.Length; i++)
        {
            int index = i;
            missionButtons[i].onClick.AddListener(() => ChangeMission(index));
        }
    }

    private void Update()
    {
        CheckMissions();
        int currentDay = DataManager.instance.nowPlayer.Playerinfo.Day;
        if (currentDay != lastCheckedDay) // 날짜가 변경되었는지 확인
        {
            UpdateMissionButtons(); // 날짜가 변경됐으면 미션 버튼 상태 업데이트
            lastCheckedDay = currentDay; // 마지막으로 확인한 날짜 업데이트
        }
    }

    void ChangeMission(int _index)
    {
        nameText.text = DataManager.instance.nowPlayer.missions[_index].Name;
        scriptText.text = DataManager.instance.nowPlayer.missions[_index].Script;
        Debug.Log(DataManager.instance.nowPlayer.missions[_index].Description);
        Debug.Log(additionalDescription[_index]);
        descriptionText.text = DataManager.instance.nowPlayer.missions[_index].Description + additionalDescription[_index];
        rewardText.text = DataManager.instance.nowPlayer.missions[_index].Reward + " Gold";
    }
    void UpdateMissionButtons()
    {

        // 현재 날짜에 따라 적절한 미션 버튼 활성화
        int currentDay = DataManager.instance.nowPlayer.Playerinfo.Day;
        if (currentDay == 1)
        {
            MissionButtons[0].SetActive(true);
            MissionButtons[1].SetActive(true);
            weeks = 1;
        }
        else if(currentDay == 8) 
        {
            if(MissionButtons[0].activeSelf || MissionButtons[1].activeSelf)
            {
                MissionOverText.SetActive(true);
                GameEvents.NotifyGameOver();
            }
            MissionButtons[2].SetActive(true);
            MissionButtons[3].SetActive(true);
            weeks = 2;
        }
        else if(currentDay == 15) 
        {
            if (MissionButtons[2].activeSelf || MissionButtons[3].activeSelf)
            {
                MissionOverText.SetActive(true);
                GameEvents.NotifyGameOver();
            }
            MissionButtons[4].SetActive(true);
            MissionButtons[5].SetActive(true);
            weeks = 3;
        }
        else if(currentDay == 22)
        {
            MissionButtons[6].SetActive(true);
            MissionButtons[7].SetActive(true);
            weeks = 4;
        }
    }
    void ResetText()
    {
        nameText.text = "";
        scriptText.text = "";
        descriptionText.text = "";
        rewardText.text = "";
    }
    #region Missions
    void CheckMissions()
    {
        if(weeks == 1) 
        {
            if (DataManager.instance.nowPlayer.missions[0].DidMission == false) Mission0();
            if (DataManager.instance.nowPlayer.missions[1].DidMission == false) Mission1(); 
        }
        else if(weeks == 2) {
            if (DataManager.instance.nowPlayer.missions[2].DidMission == false) Mission2();
            if (DataManager.instance.nowPlayer.missions[3].DidMission == false) Mission3(); 
        }
        else if(weeks == 3) {
            if (DataManager.instance.nowPlayer.missions[4].DidMission == false) Mission4();
            if (DataManager.instance.nowPlayer.missions[5].DidMission == false) Mission5(); 
        }
        else if(weeks == 4) {
            if (DataManager.instance.nowPlayer.missions[6].DidMission == false) Mission6();
            if (DataManager.instance.nowPlayer.missions[7].DidMission == false) Mission7(); 
        }
    }
    void Mission0()
    {
        if (DataManager.instance.nowPlayer.Playerinfo.Gold < 3000)
        {
            additionalDescription[0] = $"{DataManager.instance.nowPlayer.Playerinfo.Gold}Gold / 3000Gold";
        }
        else if(DataManager.instance.nowPlayer.Playerinfo.Gold >= 3000)
        {
            if (DataManager.instance.nowPlayer.missions[0].DidMission == false)
            {
                AfterMission(0);
                DataManager.instance.nowPlayer.Playerinfo.Gold += 1000;
            }
        }
    }
    void Mission1()
    {
        PlayerInventory beerItem = DataManager.instance.nowPlayer.inventory.Find(invItem => invItem.Name == "Beer");
        if (beerItem != null)
        {
            if (beerItem.Quantity < 5)
            {
                additionalDescription[1] = $"{beerItem.Quantity} / 5";
            }
            else
            {
                if (DataManager.instance.nowPlayer.missions[1].DidMission == false)
                {
                    AfterMission(1);
                    DataManager.instance.nowPlayer.Playerinfo.Gold += 1000;
                }
            }
        }
        else { additionalDescription[1] = "0 / 5"; }
    }
    void Mission2()
    {
        Item foodItem = DataManager.instance.nowPlayer.items.Find(invItem => invItem.Name == "Food");
        if (foodItem != null)
        {
            if (foodItem.EnhancementValue < 3)
            {
                additionalDescription[2] = $"{foodItem.EnhancementValue} / 3";
            }
            else
            {
                if (DataManager.instance.nowPlayer.missions[2].DidMission == false)
                {
                    AfterMission(2);
                    DataManager.instance.nowPlayer.Playerinfo.Gold += 1000;
                }
            }
        }
        else
        {
            additionalDescription[2] = "0 / 3";
        }
    }
    void Mission3()
    {
        if (foodCount < 30)
        {
            additionalDescription[3] = $"{foodCount} / 30";
        }
        else
        {
            if (DataManager.instance.nowPlayer.missions[3].DidMission == false)
            {
                AfterMission(3);
                DataManager.instance.nowPlayer.Playerinfo.Gold += 3500;
            }
        }
    }

    void Mission4()
    {
        if (whiskyCount < 5)
        {
            additionalDescription[4] = $"{whiskyCount} / 5";
        }
        else
        {
            if (DataManager.instance.nowPlayer.missions[4].DidMission == false)
            {
                AfterMission(4);
                DataManager.instance.nowPlayer.Playerinfo.Gold += 2000;
            }
        }
    }
    void Mission5()
    {
        if (DataManager.instance.nowPlayer.Playerinfo.Debt > 40000)
        {
            additionalDescription[5] = $"{DataManager.instance.nowPlayer.Playerinfo.Debt} / 40,000G";
        }
        else
        {
            if (DataManager.instance.nowPlayer.missions[5].DidMission == false)
            {
                AfterMission(5);
                DataManager.instance.nowPlayer.Playerinfo.Gold += 2000;
            }
        }
    }
    void Mission6()
    {
        PlayerInventory beerItem = DataManager.instance.nowPlayer.inventory.Find(invItem => invItem.Name == "Beer");
        if (beerItem.Quantity < 20)
        {
            additionalDescription[6] = $"{beerItem.Quantity} / 20";
        }
        else
        {
            beerItem.Quantity -= 20;
            if (beerItem.Quantity <= 0)
            {
                DataManager.instance.nowPlayer.inventory.Remove(beerItem);
            }
            if (DataManager.instance.nowPlayer.missions[6].DidMission == false)
            {
                AfterMission(6);
                DataManager.instance.nowPlayer.Playerinfo.Gold += 2000;
            }
        }

    }
    void Mission7()
    {
        PlayerInventory wineItem = DataManager.instance.nowPlayer.inventory.Find(invItem => invItem.Name == "Wine");
        if (wineItem.Quantity < 30)
        {
            additionalDescription[6] = $"{wineItem.Quantity} / 30";
        }
        else
        {
            wineItem.Quantity -= 30;
            if (wineItem.Quantity <= 0)
            {
                DataManager.instance.nowPlayer.inventory.Remove(wineItem);
            }
            if (DataManager.instance.nowPlayer.missions[7].DidMission == false)
            {
                AfterMission(7);
                DataManager.instance.nowPlayer.Playerinfo.Gold += 5000;
            }
        }
    }

    void AfterMission(int missionIndex)
    {
        ResetText();
        Debug.Log("MissionIndex"+missionIndex);
        MissionButtons[missionIndex].SetActive(false);
        DataManager.instance.nowPlayer.missions[missionIndex].DidMission = true;
    }
    #endregion

    public void AddMenuCount(Item item) 
    {
        if(item.Name == "Food")
        {
            foodCount++;
        }
        else if (item.Name == "whisky")
        {
            whiskyCount++;
        }
    }

    public void RemoveMenuCount() 
    {
        foodCount = 0;
        whiskyCount = 0;
    }
}
