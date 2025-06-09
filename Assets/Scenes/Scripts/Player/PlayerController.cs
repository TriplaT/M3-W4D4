using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private float horizontal;
    private float vertical;
    private Rigidbody2D rb;

    public Vector2 Direction { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Direction = new Vector2(horizontal, vertical).normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Direction * speed * Time.fixedDeltaTime);
    }
}
