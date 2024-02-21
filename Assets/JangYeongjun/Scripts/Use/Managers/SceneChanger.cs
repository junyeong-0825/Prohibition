using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    public static SceneChanger sceneInstance;
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
    [SerializeField] GameObject playerButton;
    internal AsyncOperation asyncLoad;

    private void Start()
    {
        AudioManager.audioInstance.PlayTitleSound();
    }
    public void ChangeToNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            if (nextSceneIndex == 1)
            {
                playerButton.SetActive(true);
            }
        }
        else
        {
            Debug.Log("No Scene");
        }
    }
    /*
    void SceneLoad(int nextSceneIndex)
    {
        SceneManager.LoadScene(nextSceneIndex);
    }

    void AsyncScnenLoad(int nextSceneIndex)
    {
        asyncLoad = SceneManager.LoadSceneAsync(nextSceneIndex);
        if (nextSceneIndex == 2)
        {
            playerButton.SetActive(true);
        }
    }
    */
}
