using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections.Generic;
using System;



[System.Serializable]
public class GoogleData
{
    public string order;
    public string result;
    public string msg;
    public int gold;
    public int debt;
    public InventoryWrapper inven;
    public ItemWrapper item;
}


public class LoginManager:MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbyHYEN4nFajcE5Ra9XT8zAhKqoZjQq2-GlgXOYq0waD-6a7w2AAMP9MWWuRSC3TNbN1sw/exec";
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

    void Login()
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
                DataManager.instance.nowPlayer.Playerinfo.Gold = GD.gold;
                DataManager.instance.nowPlayer.Playerinfo.Debt = GD.debt;
                DataManager.instance.nowPlayer.inventory = playerInventories;
                DataManager.instance.nowPlayer.items = playerItems;

                LoadingPage.SetActive(true);
                LoginPage.SetActive(false);
            }
        }
    }
}
