using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class PlayerAnimation : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Rigidbody2D rb;

    [Header("Animation Parameters")]
    [SerializeField] private float moveThreshold = 0.1f;
    [SerializeField] private float directionChangeThreshold = 0.2f;
    [SerializeField] private bool enableDebugLogs = true;

    private Vector2 lastPosition;
    private Vector2 currentDirection;
    private Vector2 lastDirection;
    private bool wasMoving;

    private void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (playerController == null) playerController = GetComponent<PlayerController>();
        if (rb == null) rb = GetComponent<Rigidbody2D>();

        ValidateAnimatorSetup();
    }

    private void ValidateAnimatorSetup()
    {
        if (animator.runtimeAnimatorController == null)
        {
            Debug.LogError("No Animator Controller assigned!", this);
            enabled = false;
            return;
        }

        if (!animator.enabled)
        {
            Debug.LogWarning("Animator component is disabled!", this);
        }
    }

    private void Update()
    {
        if (!animator.enabled) return;

        UpdateMovementState();
        UpdateDirection();
        HandleSpriteFlip();
    }

    private void UpdateMovementState()
    {
        Vector2 positionChange = (Vector2)transform.position - lastPosition;
        bool isMoving = positionChange.magnitude > moveThreshold;

        if (isMoving != wasMoving)
        {
            animator.SetBool("isMoving", isMoving);
            wasMoving = isMoving;

            if (enableDebugLogs)
                Debug.Log($"Movement state changed: {(isMoving ? "Moving" : "Idle")}");
        }

        if (isMoving)
        {
            float speedPercent = Mathf.Clamp01(rb.velocity.magnitude / playerController.MaxSpeed);
            animator.SetFloat("moveSpeed", speedPercent);
        }

        lastPosition = transform.position;
    }

    private void UpdateDirection()
    {
        currentDirection = playerController.GetMoveInput();

        if (Vector2.Distance(currentDirection, lastDirection) > directionChangeThreshold)
        {
            animator.SetFloat("xDirection", currentDirection.x);
            animator.SetFloat("yDirection", currentDirection.y);
            lastDirection = currentDirection;
        }
    }

    private void HandleSpriteFlip()
    {
        if (currentDirection.x != 0)
        {
            spriteRenderer.flipX = currentDirection.x < 0;
        }
    }

    public void PlayAttackAnimation()
    {
        if (!animator.enabled) return;

        animator.ResetTrigger("Attack");
        animator.SetTrigger("Attack");

        if (enableDebugLogs)
            Debug.Log("Attack animation triggered");
    }

    public void PlayDeathAnimation()
    {
        if (!animator.enabled) return;

        animator.SetBool("isDead", true);
        animator.Play("Death", 0, 0);

        if (enableDebugLogs)
            Debug.Log("Death animation triggered");
    }


    public void OnAttackHitFrame()
    {
        if (enableDebugLogs)
            Debug.Log("Attack hit frame reached");
    }

    public void OnDeathAnimationComplete()
    {
        if (enableDebugLogs)
            Debug.Log("Death animation completed");
    }

    public string GetCurrentAnimationState()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            return "Death";
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            return "Attack";
        return animator.GetBool("isMoving") ? "Run" : "Idle";
    }
}