using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    [SerializeField] Button saveButton;
    private void Start()
    {
        //saveButton.onClick.AddListener(() => { DataManager.instance.SaveAllData(); });
        saveButton.onClick.AddListener(() => { DataManager.instance.SetValue(); });
    }
}
