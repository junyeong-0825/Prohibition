using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExitButton : MonoBehaviour
{
    public void ExitGame()
    {
        // 에디터에서 실행 중일 때
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // 실제 게임에서 실행 중일 때
            Application.Quit();
#endif
    }
}
