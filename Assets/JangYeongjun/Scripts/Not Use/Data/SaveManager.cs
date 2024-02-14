/*
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SaveManager : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbwwLV0pazBvZoMLZl0qBztyJRCpgrDU2iGGrpWPdc_J_vHO5Epp5cmhJMg5kADQm6TWOA/exec";
    public GoogleData GD;
    public TextMeshProUGUI ErrorText;
    public TMP_InputField ValueInput;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnApplicationQuit()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "logout");
        SetValue();

        StartCoroutine(Post(form));
    }
    public void SetValue()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "setValue");
        form.AddField("value", GD.value);

        StartCoroutine(Post(form));
    }


    public void GetValue()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "getValue");

        StartCoroutine(Post(form));
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();

            if (www.isDone) Response(www.downloadHandler.text);
            else ErrorText.text = "웹의 응답이 없습니다.";
        }
    }


    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        GD = JsonUtility.FromJson<GoogleData>(json);

        if (GD.result == "ERROR")
        {
            ErrorText.text = $"{GD.order} 을 실행할 수 없습니다. 에러 메시지 : {GD.msg}";
            return;
        }

        print(GD.order + "을 실행했습니다. 메시지 : " + GD.msg);

        if (GD.order == "getValue")
        {
            ValueInput.text = GD.value;
        }
    }
}
*/