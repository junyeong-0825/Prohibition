using TMPro;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    int dialoguesCount;
    [SerializeField] TextMeshProUGUI informationText;
    [SerializeField] DialoguesSO hintDialoguesSO;

    private void OnEnable()
    {
        OnDialogues();
    }
    void OnDialogues()
    {
        dialoguesCount = Random.Range(0, hintDialoguesSO.dialogues.Length);
        float randomRed = Random.Range(0, 256) / 255f;
        float randomGreen = Random.Range(0, 256) / 255f;
        float randomBlue = Random.Range(0, 256) / 255f;
        informationText.color = new Color(randomRed, randomGreen, randomBlue, 1);
        informationText.text = hintDialoguesSO.dialogues[dialoguesCount].dialogue;
    }
}
