using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections.Generic;
using System;
using static UnityEditor.Progress;



[System.Serializable]
public class GoogleData
{
    public string order;
    public string result;
    public string msg;
    public int gold;
    public int debt;
    public bool tutorial;
    public InventoryWrapper inven;
    public ItemWrapper item;
}


public class LoginManager:MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbz7KPFDVPPsd4LduMIBZUVu4vuetTIAnDLpY5lEmexFGOYxdrQ_DLaMhuyT7K8YaKyh8w/exec";
    public TextMeshProUGUI ErrorText;
    public TMP_InputField IDInput, PassInput;
    string id, pass;
    public GameObject LoadingPage;
    public GameObject LoginPage;
    public GameObject LoginLoading;
    GoogleData GD;

    public static LoginManager loginInstance;

    private void Awake()
    {
        if (loginInstance == null)
        {
            loginInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    bool SetIDPass()
    {
        id = IDInput.text.Trim();
        pass = PassInput.text.Trim();

        if (id == "" || pass == "") return false;
        else return true;
    }

    public void Login()
    {
        if (!SetIDPass())
        {
            ErrorText.text = "아이디 또는 비밀번호가 비어있습니다";
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "login");
        form.AddField("id", id);
        form.AddField("pass", pass);
        
        LoginLoading.SetActive(true);

        StartCoroutine(Post(form));
    }

    void Register()
    {
        if (!SetIDPass())
        {
            Debug.Log("아이디 또는 비밀번호가 비어있습니다");
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "register");
        form.AddField("id", id);
        form.AddField("pass", pass);

        StartCoroutine(Post(form));
    }

    void Logout()
    {
        if (!SetIDPass())
        {
            Debug.Log("아이디 또는 비밀번호가 비어있습니다");
            return;
        }
        WWWForm form = new WWWForm();
        form.AddField("order", "logout");

        StartCoroutine(Post(form));
    }

    void OnApplicationQuit()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "logout");

        StartCoroutine(Post(form));
    }
    /*
    void SetValue()
    {
        WWWForm form = new WWWForm();

        string goldValue = DataManager.instance.nowPlayer.Playerinfo.Gold.ToString();
        if (goldValue == null) goldValue = "";
        Debug.Log(goldValue);

        string debtValue = DataManager.instance.nowPlayer.Playerinfo.Debt.ToString();
        if (debtValue == null) debtValue = "";
        Debug.Log(debtValue);

        string invenValue = JsonUtility.ToJson(DataManager.instance.nowPlayer.inventory);
        if (invenValue == null) invenValue = "";
        Debug.Log(invenValue);

        form.AddField("order", "setValue");
        form.AddField("gold", goldValue);
        form.AddField("debt", debtValue);
        form.AddField("inven", invenValue);
        StartCoroutine(Post(form));
    }
    */

    void GetValue()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "getValue");

        StartCoroutine(Post(form));
    }

    public IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            Debug.Log("Doing Request");
            www.timeout = 10; // 30초 후에 타임아웃이 발생하도록 설정

            yield return www.SendWebRequest();

            Debug.Log("DidRequest");

            // 네트워크 오류 또는 프로토콜(HTPP) 오류 확인
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                if (www.error == "Request timeout") // 타임아웃 오류 메시지 확인 (이 부분은 실제로 www.error를 확인하는 추가 로직이 필요할 수 있음)
                {
                    LoginLoading.SetActive(false);
                    ErrorText.text = "요청 시간이 초과되었습니다."; // 타임아웃 시 사용자에게 알림
                }
                else
                {
                    LoginLoading.SetActive(false);
                    ErrorText.text = $"오류 발생: {www.error}"; // 다른 종류의 오류 메시지 처리
                }
            }
            else if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Response: " + www.downloadHandler.text);
                Response(www.downloadHandler.text); // 정상적인 응답 처리
            }
            else
            {
                LoginLoading.SetActive(false);
                ErrorText.text = "알 수 없는 오류 발생.";
            }
        }
    }


    void Response(string json)
    {
        Debug.Log("Response");
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log("NullJoson");
            LoginLoading.SetActive(false);
            return;
        }
        Debug.Log(json);
        GD = JsonUtility.FromJson<GoogleData>(json);

        if (GD.result == "ERROR")
        {
            LoginLoading.SetActive(false);
            ErrorText.text = $"{GD.order} 을 실행할 수 없습니다. 에러 메시지 : {GD.msg}";
            return;
        }
        else
        {
            Debug.Log(GD.order + "을 실행했습니다. 메시지 : " + GD.msg);

            if (GD.order == "login")
            {
                GetValue();
            }
            else if (GD.order == "getValue")
            {
                List<PlayerInventory> playerInventories = GD.inven.inventory;
                List<Item> playerItems = GD.item.items;
                if ( playerInventories != null && playerItems != null) Debug.Log("Item Not Null");
                else Debug.Log("Item Data Null");
                Debug.Log($"GD.inven.inventory Count: {GD.inven.inventory.Count}");
                Debug.Log($"GD.item.items: {GD.item.items.Count}");
                Debug.Log($"playerInventories Count: {playerInventories.Count}");
                Debug.Log($"playerItems Count: {playerItems.Count}");
                DataManager.instance.nowPlayer.Playerinfo.Gold = GD.gold;
                Debug.Log(GD.gold);
                DataManager.instance.nowPlayer.Playerinfo.Debt = GD.debt;
                DataManager.instance.nowPlayer.Playerinfo.DidTutorial = GD.tutorial;
                DataManager.instance.nowPlayer.inventory = playerInventories;
                DataManager.instance.nowPlayer.items = playerItems;

                LoadingPage.SetActive(true);
                LoginPage.SetActive(false);
            }
        }
    }
}
