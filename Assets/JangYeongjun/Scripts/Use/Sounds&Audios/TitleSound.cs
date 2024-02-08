using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSound : MonoBehaviour
{
    private void Start()
    {
        AudioManager.audioInstance.PlayTitleSound();
    }
    
}
