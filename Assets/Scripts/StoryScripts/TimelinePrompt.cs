using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelinePrompt : MonoBehaviour
{
    [SerializeField] private StoryManager storyManager;
    [SerializeField] private int storyIndex = 0;
    private enum FireType
    {
        instant,
        interaction
    }
    [SerializeField] private FireType fireType;

    void Start()
    {
        LoadTimeline();
    }

    public void LoadTimeline()
    {
        Story story = storyManager.stories[storyIndex];
        if (story.storylineStatus == Story.StorylineStatus.ready)
        {
            var interact = GetComponent<Interactable>();
            interact.SetInteractable(story.playable);
            if (fireType == FireType.instant) interact.Interacting();
            storyManager.stories[storyIndex].storylineStatus = Story.StorylineStatus.ended;
        }
    }
}
