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
    [SerializeField] DialoguesSO dialoguesSO;

    private void OnEnable()
    {
        StartCoroutine(OnDialogues());
    }
    IEnumerator OnDialogues()
    {
        while (true)
        {
            dialoguesCount = Random.Range(0, dialoguesSO.dialogues.Length);
            if (dialoguesSO.dialogues[dialoguesCount].value == 1)
            {
                dialoguesText.color = new Color(1, 1, 1, 1);
                dialoguesText.text = dialoguesSO.dialogues[dialoguesCount].dialogue;
                nameText.text = dialoguesSO.dialogues[dialoguesCount].character;
                yield return new WaitForSecondsRealtime(2f);
            }
        }
    }
}
