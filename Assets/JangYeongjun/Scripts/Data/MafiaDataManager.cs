using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MafiaDataManager : MonoBehaviour
{
    const string DialogueURL = "https://docs.google.com/spreadsheets/d/13vtl_xZLrGFk1j-iw-JTqoMaoQpEFXKu0iNLsITKjyo/export?format=tsv&gid=0&range=A2:C";

    [SerializeField]DialoguesSO dialoguesSO;
    void Awake()
    {
        StartCoroutine(Get());
    }

    IEnumerator Get()
    {
        UnityWebRequest www = UnityWebRequest.Get(DialogueURL);
        yield return www.SendWebRequest();

        DialogueSO(www.downloadHandler.text);
    }

    void DialogueSO(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowsize = row.Length;
        int columnsize = row[0].Split('\t').Length;

        for (int i = 0; i < rowsize; i++) 
        {
            string[] column = row[i].Split('\t');
            for (int j = 0; j < columnsize; j++)
            {
                Dialogues dialogues = dialoguesSO.dialogues[i];
                dialogues.value = int.Parse(column[0]);
                dialogues.character = column[1];
                dialogues.dialogue = column[2];
            }
        }
    }
}
