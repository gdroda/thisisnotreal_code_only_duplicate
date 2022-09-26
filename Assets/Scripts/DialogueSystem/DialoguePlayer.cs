using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class DialoguePlayer : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] PositionManager positionManager;
    [SerializeField] TextMeshProUGUI nameTextLeft;
    [SerializeField] TextMeshProUGUI dialogueTextLeft;
    [SerializeField] Image portraitImageLeft;
    [SerializeField] Button buttonLeft;
    [SerializeField] TextMeshProUGUI nameTextRight;
    [SerializeField] TextMeshProUGUI dialogueTextRight;
    [SerializeField] Image portraitImageRight;
    [SerializeField] Button buttonRight;
    private PlayerInputActions playerInput;
    private Animator animator;
    private Queue<string> sentences;
    private Dialogue currentDialogue;
    private Dialogue[] currentSequence;
    private int sequenceIndex = 0;
    private bool inSequence = false;
    [SerializeField] private PlayableDirector director;

    private void Awake()
    {
        playerInput = new PlayerInputActions();
        playerInput.dialogue.nextDialogue.performed += ctx => DisplayNextSentence();
    }

    void Start()
    {
        sentences = new Queue<string>();
        animator = GetComponent<Animator>();
        playerInput.dialogue.nextDialogue.Disable();
    }

    private void OnEnable()
    {
        dialogueManager.dialogueEvent.AddListener(PlayDialogue);
        dialogueManager.dialogueSequenceEvent.AddListener(DialogueSequence);
        playerInput.dialogue.Enable();
    }

    private void OnDisable()
    {
        dialogueManager.dialogueEvent.RemoveListener(PlayDialogue);
        dialogueManager.dialogueSequenceEvent.RemoveListener(DialogueSequence);
        playerInput.dialogue.Disable();
    }

    private void DialogueSequence(Dialogue[] dialogues)
    {
        currentSequence = dialogues;
        inSequence = true;
        if (dialogues.Length > sequenceIndex)
        {
            PlayDialogue(dialogues[sequenceIndex]);
            sequenceIndex++;
        }
        else
        {
            sequenceIndex = 0;
            inSequence = false;
            positionManager.MovementToggle();
            EndDialogue();
        }
    }

    private void PlayDialogue(Dialogue dialogue)
    {
        dialogueManager.onDialogue = true;
        animator.SetBool("IsOpen", true);
        playerInput.dialogue.nextDialogue.Enable();
        if (director != null) director.playableGraph.GetRootPlayable(0).SetSpeed(0);

        if (dialogue.side == Dialogue.Side.left)
        {
            EnableLeft();
            nameTextLeft.text = dialogue.npcName;
            portraitImageLeft.sprite = dialogue.npcIcon;
        }
        else if (dialogue.side == Dialogue.Side.right)
        {
            EnableRight();
            nameTextRight.text = dialogue.npcName;
            portraitImageRight.sprite = dialogue.npcIcon;
        }
        //nameText.text = dialogue.npcName;
        //portraitImage.sprite = dialogue.npcIcon;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        currentDialogue = dialogue;
        if (dialogue.side == Dialogue.Side.left) DisplayNextSentence();
        else if (dialogue.side == Dialogue.Side.right) DisplayNextSentence();

        positionManager.MovementToggle();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            if (inSequence)
            {
                positionManager.MovementToggle();
                DialogueSequence(currentSequence);
            }
            else EndDialogue();
            return;
        }
        
        string sentence = sentences.Peek();
        StopAllCoroutines();

        if (currentDialogue.side == Dialogue.Side.left)
        {
            if (dialogueTextLeft.text.ToCharArray().Length < sentence.ToCharArray().Length)
            {
                dialogueTextLeft.text = sentence;
            }
            else if (dialogueTextLeft.text.ToCharArray().Length == sentence.ToCharArray().Length)
            {
                sentences.Dequeue();
                DisplayNextSentence();
            }
            else StartCoroutine(TypeSentence(sentence));
        }
        else if (currentDialogue.side == Dialogue.Side.right)
        {
            if (dialogueTextRight.text.ToCharArray().Length < sentence.ToCharArray().Length)
            {
                dialogueTextRight.text = sentence;
            }
            else if (dialogueTextRight.text.ToCharArray().Length == sentence.ToCharArray().Length)
            {
                sentences.Dequeue();
                DisplayNextSentence();
            }
            else StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        if (currentDialogue.side == Dialogue.Side.left)
        {
            dialogueTextLeft.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueTextLeft.text += letter;
                yield return new WaitForSeconds(0.025f);
            }
        }
        else if (currentDialogue.side == Dialogue.Side.right)
        {
            dialogueTextRight.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueTextRight.text += letter;
                yield return new WaitForSeconds(0.025f);
            }
        }
    }

    private void EnableLeft()
    {
        nameTextLeft.gameObject.SetActive(true);
        nameTextRight.gameObject.SetActive(false);
        dialogueTextLeft.gameObject.SetActive(true);
        dialogueTextRight.gameObject.SetActive(false);
        portraitImageLeft.gameObject.SetActive(true);
        portraitImageRight.gameObject.SetActive(false);
        buttonLeft.gameObject.SetActive(true);
        buttonRight.gameObject.SetActive(false);
    }

    private void EnableRight()
    {
        nameTextLeft.gameObject.SetActive(false);
        nameTextRight.gameObject.SetActive(true);
        dialogueTextLeft.gameObject.SetActive(false);
        dialogueTextRight.gameObject.SetActive(true);
        portraitImageLeft.gameObject.SetActive(false);
        portraitImageRight.gameObject.SetActive(true);
        //portraitImageRight.transform.localScale = new Vector3(-1f, 1f, 1f);
        buttonLeft.gameObject.SetActive(false);
        buttonRight.gameObject.SetActive(true);
    }

    private void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        positionManager.MovementToggle();
        playerInput.dialogue.nextDialogue.Disable();
        dialogueManager.onDialogue = false;
        if (director != null) director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}
