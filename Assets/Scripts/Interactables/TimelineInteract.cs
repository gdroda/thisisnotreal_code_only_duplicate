using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "TimelineInteract", menuName = "Interacts/Timeline Interact", order = 1)]
public class TimelineInteract : BaseInteract
{
    [SerializeField] PlayableAsset timelineToPlay;
    [SerializeField] TimelineManager timelineManager;

    public override void Interact()
    {
        timelineManager.SetTimeline(timelineToPlay);
    }
}
