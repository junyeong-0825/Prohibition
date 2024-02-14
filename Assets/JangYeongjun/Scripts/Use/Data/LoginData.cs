using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections.Generic;
using UnityEditor.PackageManager;



[System.Serializable]
public class GoogleData
{
    public string order;
    public string result;
    public string msg;
    /*
    public int gold;
    public int debt;
    public List<PlayerInventory> inven;
    */
}


public class LoginData : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbwwLV0pazBvZoMLZl0qBztyJRCpgrDU2iGGrpWPdc_J_vHO5Epp5cmhJMg5kADQm6TWOA/exec";
    public TextMeshProUGUI ErrorText;
    public TMP_InputField IDInput, PassInput;
    string id, pass;
    int InputSelect;
    public GameObject LoadingPage;
    public GameObject LoginPage;
    public GameObject LoginLoading;
    //string InventoryData = JsonUtility.ToJson(DataManager.instance.nowPlayer.inventory);

    #region InputField�� Tab���� �̵��ϴ� ����
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InputSelect++;
            if (InputSelect > 1) { InputSelect = 0; }
            SelectInputField();
        }
        void SelectInputField()
        {
            if (InputSelect == 0) { IDInput.Select(); }
            else if (InputSelect == 1) {  PassInput.Select(); }
        }
    }
    public void IDInputSelected() => InputSelect = 0;
    public void PasswordSelected() => InputSelect = 1;
    #endregion


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
            ErrorText.text = "���̵� �Ǵ� ��й�ȣ�� ����ֽ��ϴ�";
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "login");
        form.AddField("id", id);
        form.AddField("pass", pass);
        
        LoginLoading.SetActive(true);

        StartCoroutine(Post(form));
    }

    /*
    public void Register()
    {
        if (!SetIDPass())
        {
            print("���̵� �Ǵ� ��й�ȣ�� ����ֽ��ϴ�");
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "register");
        form.AddField("id", id);
        form.AddField("pass", pass);

        StartCoroutine(Post(form));
    }

    public void Logout()
    {
        if (!SetIDPass())
        {
            print("���̵� �Ǵ� ��й�ȣ�� ����ֽ��ϴ�");
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

    public void SetValue()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "setValue");
        form.AddField("gold", DataManager.instance.nowPlayer.Playerinfo.Gold);
        form.AddField("debt", DataManager.instance.nowPlayer.Playerinfo.Debt);
        form.AddField("inven", InventoryData);
        StartCoroutine(Post(form));
    }


    public void GetValue()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "getValue");

        StartCoroutine(Post(form));
    }
    */

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // �ݵ�� using�� ����Ѵ�
        {
            yield return www.SendWebRequest();

            if (www.isDone) Response(www.downloadHandler.text);
            else ErrorText.text = "���� ������ �����ϴ�.";
        }
    }


    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        GoogleData GD = JsonUtility.FromJson<GoogleData>(json);

        if (GD.result == "ERROR")
        {
            LoginLoading.SetActive(false);
            ErrorText.text = $"{GD.order} �� ������ �� �����ϴ�. ���� �޽��� : {GD.msg}";
            return;
        }
        else
        {
            Debug.Log(GD.order + "�� �����߽��ϴ�. �޽��� : " + GD.msg);

            if (GD.order == "login")
            {
                LoadingPage.SetActive(true);
                LoginPage.SetActive(false);
            }
            /*
            else if (GD.order == "getValue")
            {
                DataManager.instance.nowPlayer.Playerinfo.Gold = GD.gold;
                DataManager.instance.nowPlayer.Playerinfo.Debt = GD.debt;
                DataManager.instance.nowPlayer.inventory = GD.inven;
            }
            */
        }
    }
}
