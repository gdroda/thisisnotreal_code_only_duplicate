using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleHolder : MonoBehaviour
{
    [SerializeField] private string bubbleLine;

    public string GetBubble()
    {
        return bubbleLine;
    }

    private void Start()
    {
        
    }
}
