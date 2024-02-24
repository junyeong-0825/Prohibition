using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayerInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldSliderText;

    void Update()
    {
        goldSliderText.text = $"{DataManager.instance.nowPlayer.Playerinfo.Gold} gold";
    }
}
