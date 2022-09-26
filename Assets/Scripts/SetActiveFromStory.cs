using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveFromStory : MonoBehaviour
{
    public void SetActive(GameObject item)
    {
        item.gameObject.SetActive(true);
    }
}
