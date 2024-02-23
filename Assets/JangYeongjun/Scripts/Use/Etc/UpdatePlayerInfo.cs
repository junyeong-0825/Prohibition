using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayerInfo : MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] Slider timeSlider;
    [SerializeField] Slider goldSlider;
    [SerializeField] TextMeshProUGUI timeSliderText;
    [SerializeField] TextMeshProUGUI goldSliderText;

    void Update()
    {
        TimeSliderUpdate();
        GoldSliderUpdate();
    }

    void TimeSliderUpdate()
    {
        timeSliderText.text = $"{(int)timer.limitTimeSec}s / 240s";
        timeSlider.value = timer.limitTimeSec / 240f;
    }
    void GoldSliderUpdate()
    {
        goldSliderText.text = $"{DataManager.instance.nowPlayer.Playerinfo.Gold} / 50000 gold";
        goldSlider.value = DataManager.instance.nowPlayer.Playerinfo.Gold / 50000;
    }
}
