using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonScene : MonoBehaviour
{
    [SerializeField] Button button;

    private void Start()
    {
        button.onClick.AddListener(SceneChanger);
    }

    public void SceneChanger()
    {
        SceneChange.sceneInstance.ChangeToNextScene();
    }
}
