using UnityEngine;

public class MenuButtonSound : MonoBehaviour
{
    [SerializeField] AudioClip buttonAudio; // 오디오 클립 배열
    [SerializeField] AudioSource audioSource;

    // 랜덤 오디오 클립 재생
    public void PlaySound()
    {
        audioSource.clip = buttonAudio;
        audioSource.Play();
    }
}
