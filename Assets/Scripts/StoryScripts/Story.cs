using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu]
public class Story : ScriptableObject
{
    public string storyID;
    public TimelineInteract playable;
    public enum StorylineStatus
    {
        notyet,
        ready,
        ended
    }
    public StorylineStatus storylineStatus;
}
