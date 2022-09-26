using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BubblePlayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject chatBubble;

    public void SetDialogue(string text)
    {
        chatBubble.SetActive(true);
        
        StopAllCoroutines();
        StartCoroutine(FadeBubble(text));
    }

    IEnumerator FadeBubble(string text)
    {
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.035f);
        }
        yield return new WaitForSeconds(1.5f);
        chatBubble.SetActive(false);
    }
}
