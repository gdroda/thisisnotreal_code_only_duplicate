using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DialogueManager", menuName = "Managers/Dialogue Manager", order = 1)]
public class DialogueManager : ScriptableObject
{
    [System.NonSerialized] public UnityEvent<Dialogue> dialogueEvent;
    [System.NonSerialized] public UnityEvent<Dialogue[]> dialogueSequenceEvent;
    public bool onDialogue = false;

    private void OnEnable()
    {
        if (dialogueEvent == null)
            dialogueEvent = new UnityEvent<Dialogue>();
        if (dialogueSequenceEvent == null)
            dialogueSequenceEvent = new UnityEvent<Dialogue[]>();
    }

    public void SetDialogue(Dialogue dialogue)
    {
        dialogueEvent.Invoke(dialogue);
    }

    public void SetDialogueSequence(Dialogue[] dialogueSequence)
    {
        dialogueSequenceEvent.Invoke(dialogueSequence);
    }
}
