using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ContinueBtn : MonoBehaviour
{
    [SerializeField] Button button;

    private void Start()
    {
        bool result = DataManager.instance.CheckDatas();
        if (!result) { button.interactable = false; }
        else { button.interactable = true; }
        button.onClick.AddListener(SceneChang);
    }

    public void SceneChang()
    {
        DataManager.instance.LoadAllData();
        DataManager.instance.SaveAllData();
        SceneChanger.sceneInstance.ChangeToNextScene();
    }
}
