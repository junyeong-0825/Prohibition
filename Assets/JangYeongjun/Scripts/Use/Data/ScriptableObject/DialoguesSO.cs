using UnityEngine;

[System.Serializable]
public class Dialogues
{
    public string dialogue;
}

[CreateAssetMenu(fileName = "dialogues", menuName = "Dialogues", order = 0)]
public class DialoguesSO : ScriptableObject
{
    public Dialogues[] dialogues;
}
