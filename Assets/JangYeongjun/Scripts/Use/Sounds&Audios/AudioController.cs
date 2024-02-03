using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioClip[] nightClips; //밤의 오디오 클립
    [SerializeField] AudioClip mainClips; //낮의 오디오 클립
    [SerializeField] AudioSource audioSource;
    int clipIndex = 0;

    #region  싱글톤
    public static AudioController audioInstance;

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
        DontDestroyOnLoad(gameObject);
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