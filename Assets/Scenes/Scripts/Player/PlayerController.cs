using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float normalSpeed = 5f;
    public float slowSpeed = 2f;
    private float currentSpeed;

    private Rigidbody2D rb;
    private Vector2 movement;
    private AnimationController animationController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        currentSpeed = normalSpeed;
        animationController = GetComponent<AnimationController>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animationController.UpdateMovementAnimation("player_", movement);
    }

    void FixedUpdate()
    {
        Vector2 targetPos = rb.position + movement * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPos);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("SlowTerrain"))
            currentSpeed = slowSpeed;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("SlowTerrain"))
            currentSpeed = normalSpeed;
    }
}
