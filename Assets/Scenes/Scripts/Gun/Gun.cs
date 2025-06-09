using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    public float detectionRadius = 5f;
    private float fireCooldown;

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.CompareTag("Enemy") && fireCooldown <= 0f)
            {
                Shoot(enemy.transform.position - transform.position);
                fireCooldown = fireRate;
                break;
            }
        }
    }

    void Shoot(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().direction = direction;
    }
}
