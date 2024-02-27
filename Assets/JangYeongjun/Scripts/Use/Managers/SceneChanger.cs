using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    #region 싱글톤
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
}
