using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Weapon weapon;

    void Update()
    {
        if (weapon == null) return;

        if (Input.GetButtonDown("Fire1"))
        {
            weapon.Shoot();
        }
    }

    public void PickupWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
        weapon.transform.parent = transform;
        weapon.transform.localPosition = Vector3.zero;
    }
}
