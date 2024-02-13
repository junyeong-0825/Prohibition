using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;
    public GameObject gameOverCanvas;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        OffGameOverUI();
    }
    public void OnGameOverUI()
    {
        gameOverCanvas.SetActive(true);
    }
    public void OffGameOverUI()
    {
        gameOverCanvas.SetActive(false);
    }
}
