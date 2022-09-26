using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeEmail : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI emailText;
    [SerializeField] private TextMeshProUGUI newEmailText;
    [SerializeField] private Story story;
    public void ChangeMail()
    {
        emailText.gameObject.SetActive(false);
        newEmailText.gameObject.SetActive(true);
    }

    private void Start()
    {
        if (story.storylineStatus == Story.StorylineStatus.ended)
        {
            emailText.gameObject.SetActive(false);
            newEmailText.gameObject.SetActive(true);
        }
    }
}
