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
    [SerializeField] Image timeSliderFillImage;
    [SerializeField] Sprite redFill;
    [SerializeField] Sprite blueFill;

    void Update()
    {
        TimeSliderUpdate();
        GoldSliderUpdate();
    }

    void TimeSliderUpdate()
    {
        timeSliderText.text = $"{(int)timer.limitTimeSec}s / 240s";
        timeSlider.value = timer.limitTimeSec / 240f;
        if (timer.limitTimeSec > 60f)
        {
            timeSliderFillImage.sprite = blueFill;
        }
        else
        {
            timeSliderFillImage.sprite = redFill;
        }
    }
    void GoldSliderUpdate()
    {
        goldSliderText.text = $"{DataManager.instance.nowPlayer.Playerinfo.Gold} / {DataManager.instance.nowPlayer.Playerinfo.Debt} gold";
        goldSlider.value = DataManager.instance.nowPlayer.Playerinfo.Gold / DataManager.instance.nowPlayer.Playerinfo.Debt;
    }
}
