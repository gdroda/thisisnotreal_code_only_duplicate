using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PositionManager", menuName = "Managers/Position Manager", order = 0)]
public  class PositionManager : ScriptableObject
{
    [System.NonSerialized] public UnityEvent<Vector2> lastPositionEvent;
    [System.NonSerialized] public UnityEvent<Vector2> entryPositionEvent;
    [System.NonSerialized] public UnityEvent movementToggle;
    private Vector2 lastPosition;

    private void OnEnable()
    {
        if (lastPositionEvent == null)
            lastPositionEvent = new UnityEvent<Vector2>();
        if (entryPositionEvent == null)
            entryPositionEvent = new UnityEvent<Vector2>();
        if (movementToggle == null)
            movementToggle = new UnityEvent();
    }

    public void SaveLastPosition(Vector2 pos)
    {
        lastPosition = pos;
    }

    public void SetAtLastPosition()
    {
        lastPositionEvent.Invoke(lastPosition);
    }

    public void SetEntryPosition(Vector2 pos)
    {
        entryPositionEvent.Invoke(pos);
    }

    public void MovementToggle()
    {
        movementToggle.Invoke();
    }
}
