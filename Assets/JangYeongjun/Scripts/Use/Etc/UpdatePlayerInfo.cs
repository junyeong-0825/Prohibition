using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayerInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI daysText;

    void Update()
    {
        goldText.text = $"{DataManager.instance.nowPlayer.Playerinfo.Gold} gold";
        daysText.text = $"{DataManager.instance.nowPlayer.Playerinfo.Day} Days";
    }
}
