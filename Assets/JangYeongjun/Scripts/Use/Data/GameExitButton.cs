using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExitButton : MonoBehaviour
{
    public void ExitGame()
    {
        // �����Ϳ��� ���� ���� ��
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // ���� ���ӿ��� ���� ���� ��
            Application.Quit();
#endif
    }
}
