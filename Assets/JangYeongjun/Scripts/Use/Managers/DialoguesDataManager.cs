using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class DialoguesDataManager : MonoBehaviour
{
    #region URLs
    const string DialogueURL = "https://docs.google.com/spreadsheets/d/13vtl_xZLrGFk1j-iw-JTqoMaoQpEFXKu0iNLsITKjyo/export?format=tsv&gid=0&range=A2:C";
    #endregion
    #region Fields
    [SerializeField] DialoguesSO dialoguesSO;
    public static DialoguesDataManager dialoguesInstance;
    public Action<float> OnProgressChanged;
    #endregion
    private void Awake()
    {
        #region 싱글톤
        if (dialoguesInstance == null)
        {
            dialoguesInstance = this;
        }
        else if (dialoguesInstance != this)
        {
            Destroy(gameObject);
        }
        #endregion
    }
    public void SetStart()
    {
        StartCoroutine(Get());
    }

    IEnumerator Get()
    {
        UnityWebRequest www = UnityWebRequest.Get(DialogueURL);
        yield return www.SendWebRequest();

        // 데이터 처리 시작
        OnProgressChanged?.Invoke(0.5f);

        SetDialoguesSO(www.downloadHandler.text);
        // 데이터 처리 완료
        OnProgressChanged?.Invoke(1.0f);

    }
    void SetDialoguesSO(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowsize = row.Length;

        dialoguesSO.dialogues = new Dialogues[rowsize];

        for (int i = 0; i < rowsize; i++)
        {
            string[] column = row[i].Split('\t');
            if (column.Length >= 3)
            {
                Dialogues dialogues = new Dialogues();
                dialogues.value = int.Parse(column[0]);
                dialogues.character = column[1];
                dialogues.dialogue = column[2];

                dialoguesSO.dialogues[i] = dialogues;
            }
        }
    }
}
