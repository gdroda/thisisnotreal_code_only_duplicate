using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] GameObject buttonPrompt;
    [SerializeField] SceneChanger sceneChanger;
    private PlayerInputActions playerInput;
    [SerializeField] private PositionManager positionManager;
    [SerializeField] private TimelineManager timelineManager;

    private void Awake()
    {
        playerInput = new PlayerInputActions();
        playerInput.movement.interact.performed += Interact;
    }

    private void OnEnable()
    {
        playerInput.movement.interact.Enable();
        positionManager.movementToggle.AddListener(ToggleMovement);
    }

    private void OnDisable()
    {
        playerInput.movement.interact.Disable();
        positionManager.movementToggle.RemoveListener(ToggleMovement);
    }

    private void ToggleMovement()
    {
        if (playerInput.movement.interact.enabled) playerInput.movement.interact.Disable();
        else if (!playerInput.movement.interact.enabled) playerInput.movement.interact.Enable();
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (timelineManager.TimelinePlaying() == false)
        {
            Collider2D[] hitCollider = Physics2D.OverlapBoxAll(transform.localPosition, new Vector2(0.45f, 0.8f), 0f);
            foreach (Collider2D hit in hitCollider)
            {
                if (hit.CompareTag("Interactable"))
                {
                    positionManager.SaveLastPosition(transform.position);
                    hit.transform.GetComponent<Interactable>().Interacting();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            if (timelineManager.TimelinePlaying() == false) buttonPrompt.SetActive(true);
        }
        else if (collision.CompareTag("Teleporter"))
        {
            sceneChanger.ChangeRoom(collision.GetComponent<SceneTeleportHolder>().GetScene());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            buttonPrompt.SetActive(false);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(0.45f, 0.8f, 1f));
    }
}
