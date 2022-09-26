using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneChangeStatic
{
    public static void OpenPC()
    {
        SceneManager.LoadScene("PCScene");
    }
}
