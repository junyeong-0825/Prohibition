using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] nightClips; //밤의 오디오 클립
    [SerializeField] AudioClip mainClips; //낮의 오디오 클립
    [SerializeField] public AudioSource audioSource;
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

    public IEnumerator PlayNightSound()
    {
        while (true)
        {
            audioSource.clip = nightClips[clipIndex];
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
        audioSource.clip = mainClips;
        audioSource.Play();
    }
}