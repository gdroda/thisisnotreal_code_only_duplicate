using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSequence : MonoBehaviour
{
    [SerializeField] private Dialogue[] dialogueSequence;
    [SerializeField] private DialogueManager dialogueManager;

    public void PushDialogueSequence()
    {
        dialogueManager.SetDialogueSequence(dialogueSequence);
    }
}
