using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private BaseInteract interactable;
    [SerializeField] private BubblePlayer bubblePlayer;
    [SerializeField] private InteractionManager interactionManager;
    private DialogueSequence dialogueSequence;
    private DialogueHolder dialogueHolder;
    private BubbleHolder bubbleHolder;
    private bool hasDialogue = false;
    private bool hasBubble = false;
    private bool hasDialogueSequence = false;

    private void OnEnable()
    {
        //interactionManager.callInteract.AddListener(Interacting);
    }

    private void OnDisable()
    {
        //interactionManager.callInteract.RemoveListener(Interacting);
    }

    private void Start()
    {
        if (gameObject.GetComponent<DialogueHolder>() != null)
        {
            dialogueHolder = GetComponent<DialogueHolder>();
            hasDialogue = true;
        }

        if (gameObject.GetComponent<BubbleHolder>() != null)
        {
            bubbleHolder = GetComponent<BubbleHolder>();
            hasBubble = true;
        }

        if (gameObject.GetComponent<DialogueSequence>() != null)
        {
            dialogueSequence = GetComponent<DialogueSequence>();
            hasDialogueSequence = true;
        }
    }

    public void DeleteBubble()
    {
        hasBubble = false;
        bubbleHolder = null;
    }

    public void Interacting()
    {
        if (hasBubble) bubblePlayer.SetDialogue(bubbleHolder.GetBubble());

        if (hasDialogue) dialogueHolder.PushDialogue();

        if (hasDialogueSequence) dialogueSequence.PushDialogueSequence();

        if (interactable != null) interactable.Interact();
    }

    public void SetInteractable(BaseInteract _interactable)
    {
        interactable = _interactable;
    }
}
