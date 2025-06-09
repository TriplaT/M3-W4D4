using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float lifetime = 2f;
    public Vector2 direction;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        LifeController target = other.GetComponent<LifeController>();
        if (target != null)
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
