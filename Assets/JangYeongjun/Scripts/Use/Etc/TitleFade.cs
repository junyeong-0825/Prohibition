using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleFade : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TitleName;
    float time = 0f;
    void Start()
    {
        StartCoroutine(FadeinTitle());
    }

    IEnumerator FadeinTitle()
    {
        Color color = TitleName.color;

        while (color.a < 1f)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, time);
            TitleName.color = color;
            yield return null;
        }
    }

}
