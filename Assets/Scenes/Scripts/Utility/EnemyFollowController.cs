using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyFollowController : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 3f;
    public float smoothMovement = 0.1f;

    private Rigidbody2D rb;
    private Vector2 currentVelocity;
    private AnimationController animationController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        animationController = GetComponent<AnimationController>();
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 currentPosition = rb.position;
        Vector2 targetPosition = target.position;

        Vector2 direction = (targetPosition - currentPosition).normalized;

        Vector2 newPosition = Vector2.SmoothDamp(currentPosition, currentPosition + direction * moveSpeed * Time.fixedDeltaTime, ref currentVelocity, smoothMovement);
        rb.MovePosition(newPosition);

        animationController.UpdateMovementAnimation("enemy_", direction);
    }
}
