using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        LifeManager life = collision.GetComponent<LifeManager>();
        if (life != null)
        {
            life.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
