using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumChanger : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider backgroundSlider;

    const string MIXER_MASTER = "MasterVolume";
    const string MIXER_SFX = "SFXVolume";
    const string MIXER_BACKGROUND = "BackgroundVolume";

    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(ChangeMastrVolum);
        sfxSlider.onValueChanged.AddListener(ChangeSFXVolum);
        backgroundSlider.onValueChanged.AddListener(ChangeBackgroundVolum);
    }

    void ChangeMastrVolum(float value)
    {
        audioMixer.SetFloat(MIXER_MASTER, Mathf.Log10(value)*20);
    }

    void ChangeSFXVolum(float value)
    {
        audioMixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }

    void ChangeBackgroundVolum(float value)
    {
        audioMixer.SetFloat(MIXER_BACKGROUND, Mathf.Log10(value) * 20);
    }
}
