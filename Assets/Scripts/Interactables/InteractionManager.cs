using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class InteractionManager : ScriptableObject
{
    [System.NonSerialized] public UnityEvent<BaseInteract> callInteract;

    private void OnEnable()
    {
        if (callInteract == null)
            callInteract = new UnityEvent<BaseInteract>();
    }

    public void CallInteractable(BaseInteract interact)
    {
        callInteract.Invoke(interact);
    }
}
