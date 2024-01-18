using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyGoods : MonoBehaviour
{
    int dialoguesCount = 0;
    [SerializeField] TextMeshProUGUI dialoguesText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] DialoguesSO dialoguesSO;

    private void Start()
    {
        OnDialogues();
    }
    public void OnDialogues()
    {
        dialoguesText.text = dialoguesSO.dialogues[dialoguesCount].dialogue;
        nameText.text = dialoguesSO.dialogues[dialoguesCount].character;
        dialoguesCount++;
    }
}
