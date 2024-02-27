using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerTutorialState : MonoBehaviour
{
    public void DidTutorial()
    {
        DataManager.instance.nowPlayer.Playerinfo.IsTutorialed = true;
    }
}
