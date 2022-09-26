using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameScript : ScriptableObject
{
    public Sentence[] sentences;
}

[System.Serializable]
public class Sentence
{
    public string npcName;
    [TextArea(3, 10)]
    public string text;
    public bool hasChoice;
    public string choiceOne;
    public string choiceTwo;
    public string choiceThree;
    public GameScript choiceOneScript;
    public GameScript choiceTwoScript;
    public GameScript choiceThreeScript;
    public bool finalSentence;
    public bool triggerDialogue;
}
