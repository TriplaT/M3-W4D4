using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxSpeed = 7f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 15f;
    [SerializeField][Range(0, 1)] private float vPower = 0.9f;
    [SerializeField] private float frictionAmount = 0.2f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private LayerMask groundLayer;

    [Header("Animation")]
    [SerializeField] private PlayerAnimation playerAnimation;

    private Vector2 moveInput;
    private bool isFacingRight = true;

    public float MaxSpeed => maxSpeed;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (playerCollider == null) playerCollider = GetComponent<Collider2D>();
        if (playerAnimation == null) playerAnimation = GetComponent<PlayerAnimation>();

        if (rb == null) Debug.LogError("Rigidbody2D missing!", this);
        if (playerCollider == null) Debug.LogError("Collider2D missing!", this);
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;

        if (moveInput.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            Flip();
        }

        if (Input.GetButtonDown("Fire1") && playerAnimation != null)
        {
            playerAnimation.PlayAttackAnimation();
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
        ApplyFriction();
    }

    private void HandleMovement()
    {
        Vector2 targetVelocity = moveInput * moveSpeed;

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
            return;
        }

        Vector2 velocityDifference = targetVelocity - rb.velocity;

        float accelerationRate = (Mathf.Abs(targetVelocity.magnitude) > 0.01f) ? acceleration : deceleration;

        Vector2 movementForce = velocityDifference * accelerationRate;

        rb.AddForce(movementForce * Mathf.Pow(vPower, Time.fixedDeltaTime));
    }

    private void ApplyFriction()
    {
        if (IsGrounded() && Mathf.Abs(moveInput.x) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), frictionAmount);
            amount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    public bool IsGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            playerCollider.bounds.center,
            playerCollider.bounds.size,
            0f,
            Vector2.down,
            extraHeight,
            groundLayer);

        return raycastHit.collider != null;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public bool IsMoving()
    {
        return Mathf.Abs(rb.velocity.x) > 0.1f || Mathf.Abs(rb.velocity.y) > 0.1f;
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }

    public void Die()
    {
        if (playerAnimation != null)
        {
            playerAnimation.PlayDeathAnimation();
        }
        enabled = false;
    }
}