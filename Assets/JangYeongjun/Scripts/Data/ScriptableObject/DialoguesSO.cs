using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[System.Serializable]
public class Dialogues
{
    public int value;
    public string character;
    public string dialogue;
}

[CreateAssetMenu(fileName = "Mafia", menuName = "Dialogues", order = 0)]
public class DialoguesSO : ScriptableObject
{
    public Dialogues[] dialogues;
}
