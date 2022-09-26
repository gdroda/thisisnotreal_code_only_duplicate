using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class GameDialogues : MonoBehaviour
{
    public UnityEvent onDialogueTrigger;
    [SerializeField] private GameScript gameScript;
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private Story storyToTrigger;
    private PlayerInputActions playerInput;
    private Queue<Sentence> queue;
    private bool finishedTyping = true;
    public enum DialogueState
    {
        writing,
        waitingForAnswer
    }
    DialogueState dialogueState = DialogueState.writing;

    private void Awake()
    {
        playerInput = new PlayerInputActions();
        playerInput.ingame.nextText.performed += ctx => DisplayNextText();
    }

    private void Start()
    {
        queue = new Queue<Sentence>();
        PlayText();
    }
    private void OnEnable()
    {
        playerInput.ingame.Enable();
    }

    private void OnDisable()
    {
        playerInput.ingame.Disable();
    }

    private void PlayText()
    {
        queue.Clear();
        foreach (Sentence sentence in gameScript.sentences)
        {
            queue.Enqueue(sentence);
        }
        DisplayNextText();
    }

    public void DisplayNextText()
    {
        if (dialogueManager.onDialogue) return;

        if (queue.Count == 0)
        {
            if (gameScript.sentences[gameScript.sentences.Length - 1].finalSentence)
            {
                EndText();
                return;
            }
        }

        if (dialogueState == DialogueState.writing)
        {
            if (finishedTyping)
            {
                finishedTyping = false;
                Sentence sentence = queue.Dequeue();
                StopAllCoroutines();
                StartCoroutine(TypeText(sentence));
            }
        }
    }

    IEnumerator TypeText(Sentence sentence)
    {
        //textField.text += sentence.npcName + ": ";
        foreach (char letter in sentence.text.ToCharArray())
        {
            textField.text += letter;
            yield return new WaitForSeconds(0.015f);
        }
        if (sentence.hasChoice) StartCoroutine(TypeChoices(sentence));
        textField.text += "\n\n";
        finishedTyping = true;
    }

    IEnumerator TypeChoices(Sentence sentence)
    {
        
        textField.text += "\n";
        textField.text += "1. " + sentence.choiceOne;
        if (gameScript.sentences[gameScript.sentences.Length - 1].choiceTwoScript)
        {
            textField.text += "\n";
            textField.text += "2. " + sentence.choiceTwo;
        }
        if (gameScript.sentences[gameScript.sentences.Length - 1].choiceThreeScript)
        {
            textField.text += "\n";
            textField.text += "3. " + sentence.choiceThree;
        }
        dialogueState = DialogueState.waitingForAnswer;
        if (sentence.triggerDialogue) PauseForDialogue();
        yield return null;
    }

    private void PauseForDialogue()
    {
        onDialogueTrigger.Invoke();
    }

    private void AddScript(int index)
    {
        switch (index)
        {
            case 1: 
                if (gameScript.sentences[gameScript.sentences.Length - 1].choiceOneScript)
                {
                    gameScript = gameScript.sentences[gameScript.sentences.Length - 1].choiceOneScript;
                    dialogueState = DialogueState.writing;
                    PlayText();
                }
                break;
            case 2: 
                if (gameScript.sentences[gameScript.sentences.Length - 1].choiceTwoScript)
                {
                    gameScript = gameScript.sentences[gameScript.sentences.Length - 1].choiceTwoScript;
                    dialogueState = DialogueState.writing;
                    PlayText();
                }
                break;
            case 3:
                if (gameScript.sentences[gameScript.sentences.Length - 1].choiceThreeScript)
                {
                    gameScript = gameScript.sentences[gameScript.sentences.Length - 1].choiceThreeScript;
                    dialogueState = DialogueState.writing;
                    PlayText();
                }
                break;
        }
        
    }

    private void LateUpdate() 
    {
        scrollRect.verticalNormalizedPosition = 0; //FIND A WAY TO CALL THIS WITHOUT USING UPDATE

        if (dialogueState == DialogueState.waitingForAnswer && !dialogueManager.onDialogue)
        {
            if (playerInput.ingame.choiceOne.triggered)
            {
                AddScript(1);
            }
            else if (playerInput.ingame.choiceTwo.triggered)
            {
                AddScript(2);
            }
            else if (playerInput.ingame.choiceThree.triggered)
            {
                AddScript(3);
            }
        }
    }

    private void EndText()
    {
        //set up stuff for chapter 2
        storyToTrigger.storylineStatus = Story.StorylineStatus.ready;
        sceneChanger.ChangeRoom("Room");
    }
}
