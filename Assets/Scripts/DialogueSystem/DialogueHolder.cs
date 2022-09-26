using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private Dialogue dialogue;

    public void PushDialogue()
    {
        dialogueManager.SetDialogue(dialogue);
    }
}
