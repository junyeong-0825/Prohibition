using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtonSounds : MonoBehaviour
{
    public AudioClip[] clips; // ����� Ŭ�� �迭
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // ���� ����� Ŭ�� ���
    public void PlaySound()
    {
        int clipIndex = Random.Range(0, clips.Length);
        audioSource.clip = clips[clipIndex];
        audioSource.Play();
    }
}
