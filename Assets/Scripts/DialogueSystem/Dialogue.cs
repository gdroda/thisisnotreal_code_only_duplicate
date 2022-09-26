using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    public string npcName;
    public Sprite npcIcon;
    public enum Side
    {
        left,
        right
    }
    public Side side;
    [TextArea(3,10)]
    public string[] sentences;
}
