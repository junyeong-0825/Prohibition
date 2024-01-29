using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Slider progressBar;
    public TextMeshProUGUI progressText;

    void OnEnable()
    {
        Debug.Log("On Enabled");
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TitleScene");
        Debug.Log("Scene Started");
        asyncLoad.allowSceneActivation = false;
        Debug.Log("Scene Falsed");

        while (asyncLoad.progress < 0.9f)
        {
            Debug.Log("Progress Updated");
            float progress = asyncLoad.progress / 0.9f;
            UpdateUI(progress);
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Data Before");
        
        
        yield return StartCoroutine(LoadDataAsync());

        Debug.Log("Data After");
        asyncLoad.allowSceneActivation = true;
    }
    IEnumerator LoadDataAsync()
    {
        float totalProgress = 0.0f;
        int totalManagers = 2;
        int completedManagers = 0;

        Action<float> dataProgressHandler = (progress) =>
        {
            Debug.Log("plus progress");
            totalProgress += progress;
            UpdateUI(totalProgress);
            if (progress >= 1f)
            {
                completedManagers++;
                totalProgress = completedManagers / (float)totalManagers;
                Debug.Log($"totalProgress{totalProgress}");
                Debug.Log($"completedManagers{completedManagers}");
                UpdateUI(totalProgress);
            }
        };

        DialoguesDataManager.Instance.OnProgressChanged += dataProgressHandler;
        ItemDataReading.Instance.OnProgressChanged += dataProgressHandler;
        Debug.Log($"completedManagers{completedManagers}");
        DialoguesDataManager.Instance.SetStart();
        ItemDataReading.Instance.SetStart();
        yield return new WaitUntil(() => completedManagers >= totalManagers);

        DialoguesDataManager.Instance.OnProgressChanged -= dataProgressHandler;
        ItemDataReading.Instance.OnProgressChanged -= dataProgressHandler;
    }

    void UpdateUI(float progress)
    {
        progressBar.value = progress;
        progressText.text = (progress * 100).ToString("F0") + "%";
    }
}
