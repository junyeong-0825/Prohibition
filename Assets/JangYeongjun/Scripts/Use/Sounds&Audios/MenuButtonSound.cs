using UnityEngine;

public class MenuButtonSound : MonoBehaviour
{
    [SerializeField] AudioClip buttonAudio; // ����� Ŭ�� �迭
    [SerializeField] AudioSource audioSource;

    // ���� ����� Ŭ�� ���
    public void PlaySound()
    {
        audioSource.clip = buttonAudio;
        audioSource.Play();
    }
}
