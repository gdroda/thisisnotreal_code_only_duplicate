using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "TimelineManager", menuName = "Managers/Timeline Manager", order = 2)]
public class TimelineManager : ScriptableObject
{
    [System.NonSerialized] public UnityEvent<PlayableAsset> timelineEvent;
    [SerializeField] private bool isTimelinePlaying = false;

    private void OnEnable()
    {
        if (timelineEvent == null)
            timelineEvent = new UnityEvent<PlayableAsset>();
    }

    public void SetTimeline(PlayableAsset toPlay) //have to add the manager in a third script and call SetTimeLine to play with a PlayableAsset
    {
        timelineEvent.Invoke(toPlay);
    }

    public bool TimelinePlaying()
    {
        if (isTimelinePlaying) return true;
        else return false;
    }
    
    public void SetTimelineBool(bool status)
    {
        isTimelinePlaying = status;
    }
}
