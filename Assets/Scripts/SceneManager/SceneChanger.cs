using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private PositionManager positionManager;
    [SerializeField] private StoryManager storyManager;
    [SerializeField] private Animator animator;
    private static bool changedRooms = true;
    [SerializeField] private Story storyDone;
    [SerializeField] private Story storyToReady;

    private void Awake()
    {
        if (changedRooms)
        {
            var pos = GameObject.Find("EntryPosition_Bathroom");
            if (pos != null)
            {
                positionManager.SetEntryPosition(pos.transform.position);
            }
            changedRooms = false;
        }else
        {
            positionManager.SetAtLastPosition();
        }
    }

    private void Start()
    {
        if (storyDone != null && storyToReady != null)
        {
            if (storyDone.storylineStatus == Story.StorylineStatus.ended)
                if (storyToReady.storylineStatus == Story.StorylineStatus.ended) return;
            else storyToReady.storylineStatus = Story.StorylineStatus.ready;

        }
        
    }

    public void OpenPC()
    {
        StartCoroutine(LoadLevel("PCScene"));
        if (storyManager.stories[2].storylineStatus == Story.StorylineStatus.notyet) storyManager.stories[2].storylineStatus = Story.StorylineStatus.ready;
    }

    public void ClosePC()
    {
        StartCoroutine(LoadLevel("Room")); //has to be the room with the pc, in case of multiple, rewrite code.
    }

    public void ChangeRoom(string name)
    {
        StartCoroutine(LoadLevel(name));
        changedRooms = true;
    }

    public void CeaseMovement()
    {
        positionManager.MovementToggle();
    }

    IEnumerator LoadLevel(string name)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(0.33f);
        SceneManager.LoadScene(name);
    }

    public void ResetGame()
    {
        storyManager.stories[0].storylineStatus = Story.StorylineStatus.ended;
        storyManager.stories[1].storylineStatus = Story.StorylineStatus.ready;
        storyManager.stories[2].storylineStatus = Story.StorylineStatus.notyet;
        storyManager.stories[3].storylineStatus = Story.StorylineStatus.notyet;
        storyManager.stories[4].storylineStatus = Story.StorylineStatus.notyet;
        storyManager.stories[5].storylineStatus = Story.StorylineStatus.notyet;
        storyManager.stories[6].storylineStatus = Story.StorylineStatus.notyet;
        StartCoroutine(LoadLevel("Room"));
    }
}
