using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SceneButton : MonoBehaviour
{
    [SerializeField] Button button;

    private void Start()
    {
        button.onClick.AddListener(SceneChang);
    }

    public void SceneChang()
    {
        DataManager.instance.DeleteAllData();
        DataManager.instance.LoadAllData();
        DataManager.instance.SaveAllData();
        SceneChanger.sceneInstance.ChangeToNextScene();
    }
}
