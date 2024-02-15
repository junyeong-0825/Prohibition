/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendDataManager : MonoBehaviour
{
    const string SendURL = "https://script.google.com/macros/s/AKfycbxhLq1TEATsTiK9XJytjAeG67Oc8dOhXicc6H3TiqAOIH1QE12o7Xizj7P-ZlOd6kDi8w/exec";
    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(SendURL, form))
        {
            yield return www.SendWebRequest();
        }
    }

}
*/