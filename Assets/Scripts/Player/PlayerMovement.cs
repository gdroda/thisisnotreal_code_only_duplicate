using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputActions playerInput;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;
    [SerializeField] PositionManager positionManager;
    [SerializeField] Animator animator;

    private void Awake()
    {
        playerInput = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerInput.movement.move.Enable();
        positionManager.lastPositionEvent.AddListener(GoToLastInteractable);
        positionManager.entryPositionEvent.AddListener(GoToEntry);
        positionManager.movementToggle.AddListener(MovementToggle);
    }

    private void OnDisable()
    {
        playerInput.movement.move.Disable();
        positionManager.lastPositionEvent.RemoveListener(GoToLastInteractable);
        positionManager.entryPositionEvent.RemoveListener(GoToEntry);
        positionManager.movementToggle.RemoveListener(MovementToggle);
    }

    private void GoToEntry(Vector2 pos)
    {
        transform.position = pos;
    }

    private void GoToLastInteractable(Vector2 pos)
    {
        transform.position = pos;
    }

    private void MovementToggle()
    {
        if (playerInput.movement.move.enabled) playerInput.movement.move.Disable();
        else if (!playerInput.movement.move.enabled) playerInput.movement.move.Enable();
    }

    void FixedUpdate()
    {
        Vector2 moveInput = playerInput.movement.move.ReadValue<Vector2>();
        rb.velocity = moveInput * speed;

        if (rb.velocity.y > 0)
        {
            animator.SetBool("IsMoving", true);
            animator.SetBool("IsMovingForward", false);
        }
        else if (rb.velocity.y < 0 || rb.velocity.x != 0)
        {
            animator.SetBool("IsMoving", true);
            animator.SetBool("IsMovingForward", true);
        }
        else animator.SetBool("IsMoving", false);
    }
}
