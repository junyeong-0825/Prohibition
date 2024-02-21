using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DialoguesChanger : MonoBehaviour
{
    int dialoguesCount;
    [SerializeField] TextMeshProUGUI dialoguesText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] DialoguesSO mafiaDialoguesSO;

    private void OnEnable()
    {
        StartCoroutine(OnDialogues());
    }
    IEnumerator OnDialogues()
    {
        while (true)
        {
            dialoguesCount = Random.Range(0, mafiaDialoguesSO.dialogues.Length);
            dialoguesText.color = new Color(1, 1, 1, 1);
            dialoguesText.text = mafiaDialoguesSO.dialogues[dialoguesCount].dialogue;
            yield return new WaitForSecondsRealtime(2f);
        }
    }
}
