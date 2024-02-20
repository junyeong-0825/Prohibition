using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    [SerializeField] GameObject[] MissionButtons;

    private void Start()
    {
        foreach(var button in MissionButtons) { button.SetActive(false); }
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {

    }

}
