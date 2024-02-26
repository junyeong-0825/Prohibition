using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] nightClips; //밤의 오디오 클립
    [SerializeField] AudioClip mainClips; //낮의 오디오 클립
    [SerializeField] AudioClip titleClips;
    [SerializeField] AudioClip gameOverClip, gameClearClip;
    [SerializeField] public AudioSource audioSource;
    private Coroutine nightSoundCoroutine;
    int clipIndex = 0;

    #region  싱글톤
    public static AudioManager audioInstance;

    private void Awake()
    {
        if (audioInstance == null)
        {
            audioInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (transform.parent != null)
        {
            DontDestroyOnLoad(transform.parent.root.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion
    

    public void StartPlayNightSound()
    {
        if (nightSoundCoroutine != null)
        {
            StopCoroutine(nightSoundCoroutine);
        }
        nightSoundCoroutine = StartCoroutine(PlayNightSound());
    }

    public void StopPlayNightSound()
    {
        if (nightSoundCoroutine != null)
        {
            StopCoroutine(nightSoundCoroutine);
            nightSoundCoroutine = null;
        }
        audioSource.Stop();
    }
    public IEnumerator PlayNightSound()
    {
        audioSource.Stop();
        while (true)
        {
            audioSource.clip = nightClips[clipIndex];
            audioSource.volume = 0.1f;
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);

            clipIndex++;
            if (clipIndex >= nightClips.Length)
            {
                clipIndex = 0;
            }
        }
    }

    public void PlayMainSound()
    {
        StopPlayNightSound();
        audioSource.clip = mainClips;
        audioSource.volume = 0.1f;
        audioSource.Play();
    }

    public void PlayTitleSound()
    {
        audioSource.Stop();
        audioSource.clip = titleClips;
        audioSource.volume = 0.1f;
        audioSource.Play();
    }

    public void PlayGameOverSound()
    {
        audioSource.Stop();
        audioSource.clip = gameOverClip;
        audioSource.volume = 0.1f;
        audioSource.Play();
    }

    public void PlayGameClearSound()
    {
        audioSource.Stop();
        audioSource.clip = gameClearClip;
        audioSource.volume = 0.1f;
        audioSource.Play();
    }
}