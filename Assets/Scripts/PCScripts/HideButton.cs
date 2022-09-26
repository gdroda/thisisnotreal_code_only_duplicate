using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideButton : MonoBehaviour
{
    [SerializeField] Story story;
    [SerializeField] Button button;
    [SerializeField] GameObject thing;

    void Start()
    {
        if (button != null)
        {
            if (story.storylineStatus == Story.StorylineStatus.ended)
            {
                button.gameObject.SetActive(true);
            }
        }
        

        if (thing != null)
        {
            if (story.storylineStatus == Story.StorylineStatus.ready)
                thing.gameObject.SetActive(true);
        }
    }
}
