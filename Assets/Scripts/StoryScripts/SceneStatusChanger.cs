using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStatusChanger : MonoBehaviour
{
    public void SetReady(Story story)
    {
        if (story.storylineStatus == Story.StorylineStatus.notyet)
        {
            story.storylineStatus = Story.StorylineStatus.ready;
        }
    }

    public void SetEnded(Story story)
    {
        story.storylineStatus = Story.StorylineStatus.ended;
    }
}
