using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdatePlayerMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gold;
    [SerializeField] TextMeshProUGUI debt;
    [SerializeField] TextMeshProUGUI days;
    public void MenuUpdate()
    {
        gold.text = $"{DataManager.instance.nowPlayer.Playerinfo.Gold} Gold";
        debt.text = $"{DataManager.instance.nowPlayer.Playerinfo.Debt} Gold";
        days.text = $"{DataManager.instance.nowPlayer.Playerinfo.Day} Days";
    }
}
