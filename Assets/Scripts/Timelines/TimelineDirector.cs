using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineDirector : MonoBehaviour
{
    private PlayableDirector director;
    [SerializeField] private TimelineManager timelineManager;

    void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.played += OnScenePlay;
        director.stopped += OnSceneEnd;
        timelineManager.timelineEvent.AddListener(PlayScene); //subscribe to timeline manager this way to have function play
    }

    public void PlayScene(PlayableAsset toPlay)
    {
        director.Play(toPlay);
    }

    private void OnScenePlay(PlayableDirector obj)
    {
        timelineManager.SetTimelineBool(true);
    }

    private void OnSceneEnd(PlayableDirector obj)
    {
        timelineManager.SetTimelineBool(false);
    }
}

