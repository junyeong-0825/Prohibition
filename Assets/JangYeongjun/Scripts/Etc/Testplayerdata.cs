using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Testplayerdata : MonoBehaviour
{
    public void TestSceneChanger1()
    {
        SceneManager.LoadScene("Mafia Scene");
    }

    public void TestSceneChanger2()
    {
        SceneManager.LoadScene("Enhancment Scene");
    }

    public void TestSceneChanger3()
    {
        SceneManager.LoadScene("Store Scene");
    }
}
