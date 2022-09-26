using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedSetup : MonoBehaviour
{
    [SerializeField] Story story;
    [SerializeField] InteractionManager interactionManager;
    [SerializeField] BubbleHolder bubbleHolder;

    public void KillSleepy()
    {
        Destroy(bubbleHolder);
    }
}
