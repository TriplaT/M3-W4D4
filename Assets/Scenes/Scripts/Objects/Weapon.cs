using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 10f;
    public bool shootToRight = true; // direzione di default

    public void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bulletObj.GetComponent<Rigidbody2D>();

        Vector2 shootDirection = shootToRight ? Vector2.right : Vector2.left;
        rb.AddForce(shootDirection * bulletForce, ForceMode2D.Impulse);
    }
}
