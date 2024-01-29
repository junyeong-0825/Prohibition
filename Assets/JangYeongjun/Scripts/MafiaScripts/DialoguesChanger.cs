using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DialoguesChanger : MonoBehaviour
{
    int dialoguesCount = 0;
    [SerializeField] TextMeshProUGUI dialoguesText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] DialoguesSO dialoguesSO;
    [SerializeField] GameObject ChoicePanle;
    [SerializeField] GameObject DialoguePanel;
    public GoogleData GD;

    private void Start()
    {
        OnDialogues();
    }
    public void OnDialogues()
    {
        dialoguesText.text = dialoguesSO.dialogues[dialoguesCount].dialogue;
        nameText.text = dialoguesSO.dialogues[dialoguesCount].character;
        dialoguesCount++;

        if(dialoguesCount>7)
        {
            dialoguesCount = 0;
            GD.value += 1;
            SceneChange.instance.ChangeToNextScene();
        }
    }
}
