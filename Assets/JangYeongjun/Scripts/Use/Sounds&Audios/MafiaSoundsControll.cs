using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MafiaSoundsControll : MonoBehaviour
{
    public AudioClip[] clips; // 오디오 클립 배열
    private AudioSource audioSource;
    int clipIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySound());
    }

    // 특정 오디오 클립 재생
    IEnumerator PlaySound()
    {
        while (true)
        {
            audioSource.clip = clips[clipIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);

            clipIndex++;
            if (clipIndex >= clips.Length)
            {
                clipIndex = 0;
            }
        }
    }
}
