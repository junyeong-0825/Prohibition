using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    #region �̱���
    public static SceneManager sceneInstance;
    private void Awake()
    {
        if (sceneInstance == null)
        {
            sceneInstance = this;
        }
        else { Destroy(this.gameObject); }
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

    private void Start()
    {
        AudioManager.audioInstance.PlayTitleSound();
    }
    public void ChangeToNextScene()
    {
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
            if(nextSceneIndex == 1)
            {
                AudioManager.audioInstance.PlayLoginSound();
            }
        }
        else
        {
            Debug.Log("�� �̻� �ε��� ���� �����ϴ�.");
        }
    }
}
