using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionScriptChange : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText, descriptionText;
    [SerializeField] string[] heads;
    [SerializeField] string[] scripts;

    void ChangeMission(int _index)
    {
        nameText.text = heads[_index];
        descriptionText.text = scripts[_index];
    }
}
