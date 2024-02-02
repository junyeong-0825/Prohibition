using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MafiaSoundsControll : MonoBehaviour
{
    public AudioClip[] clips; // ����� Ŭ�� �迭
    private AudioSource audioSource;
    int clipIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySound());
    }

    // Ư�� ����� Ŭ�� ���
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
