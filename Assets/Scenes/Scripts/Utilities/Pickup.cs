using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject weaponPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject weapon = Instantiate(weaponPrefab, other.transform);
            weapon.transform.localPosition = Vector3.zero;
            Destroy(gameObject);
        }
    }
}
