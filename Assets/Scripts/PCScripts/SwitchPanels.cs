using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPanels : MonoBehaviour
{
    [SerializeField] private GameObject eMailer, game, ingame;
    
    public void OpenMail()
    {
        if (eMailer.activeSelf.Equals(true)) eMailer.SetActive(false);
        else eMailer.SetActive(true);
    }

    public void OpenGame()
    {
        if (game.activeSelf.Equals(true)) game.SetActive(false);
        else game.SetActive(true);
    }

    public void GoInGame()
    {
        if (ingame.activeSelf.Equals(true)) ingame.SetActive(false);
        else ingame.SetActive(true);
    }
}
