using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTeleportHolder : MonoBehaviour
{
    [SerializeField] private string sceneToGo;

    public string GetScene()
    {
        return sceneToGo;
    }
}
