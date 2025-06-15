using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponPrefab;

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerShooting player = collision.GetComponent<PlayerShooting>();
        if (player != null)
        {
            Weapon weaponInstance = Instantiate(weaponPrefab).GetComponent<Weapon>();
            player.PickupWeapon(weaponInstance);
            Destroy(gameObject);
        }
    }
}
