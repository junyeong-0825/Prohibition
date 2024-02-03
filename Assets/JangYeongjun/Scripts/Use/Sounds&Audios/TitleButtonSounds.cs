using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtonSounds : MonoBehaviour
{
    public AudioClip[] clips; // 오디오 클립 배열
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // 랜덤 오디오 클립 재생
    public void PlaySound()
    {
        int clipIndex = Random.Range(0, clips.Length);
        audioSource.clip = clips[clipIndex];
        audioSource.Play();
    }
}
