using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneInteract", menuName = "Interacts/Scene Interact", order = 0)]
public class SceneInteract : BaseInteract
{
    [SerializeField] private string sceneNameToChange;
    public override void Interact()
    {
        SceneManager.LoadScene(sceneNameToChange);
    }
}
