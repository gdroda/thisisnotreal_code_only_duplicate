using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryManager", menuName = "Managers/Story Manager", order = 3)]
public class StoryManager : ScriptableObject
{
    public Story[] stories;
}