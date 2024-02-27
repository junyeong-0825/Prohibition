using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;
    [SerializeField] GameObject[] canvases;

    private void OnEnable()
    {
        GameEvents.GameOver += OnGameOverUI;
        GameEvents.GameClear += OnGameClearUI;
    }
    private void OnDisable()
    {
        GameEvents.GameOver -= OnGameOverUI;
        GameEvents.GameClear -= OnGameClearUI;
    }

    void OnGameOverUI()
    {
        AudioManager.audioInstance.PlayGameOverSound();
        GameOverSetting();
        gameOverPanel.SetActive(true);
    }
    void OnGameClearUI()
    {
        AudioManager.audioInstance.PlayGameClearSound();
        GameOverSetting();
        gameClearPanel.SetActive(true);
    }
    void GameOverSetting()
    {
        foreach (var canvas in canvases) { canvas.SetActive(false); }
        DataManager.instance.DeleteAllData();
    }

    public void GoTitle()
    {
        foreach (var canvas in canvases) { canvas.SetActive(true); }
        SceneManager.LoadScene("TitleScene");
    }
}
