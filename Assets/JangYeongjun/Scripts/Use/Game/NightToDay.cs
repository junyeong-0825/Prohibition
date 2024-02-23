using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightToDay : MonoBehaviour
{
    [SerializeField] Button nightButton;

    private void Start()
    {
        nightButton.onClick.AddListener(() => NightToDayEvent());
    }

    void NightToDayEvent()
    {
        GameEvents.NotifyDayStart();
    }
}
