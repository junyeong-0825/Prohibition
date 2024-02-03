using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    public static SceneChange sceneInstance;
    
    private void Awake()
    {
        if (sceneInstance == null)
        {
            sceneInstance = this;
        }
        else { Destroy(this.gameObject); }
        DontDestroyOnLoad(this);
    }
    #endregion
    public void ChangeToNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("¥ı ¿ÃªÛ ∑ŒµÂ«“ æ¿¿Ã æ¯Ω¿¥œ¥Ÿ.");
        }
    }
}
