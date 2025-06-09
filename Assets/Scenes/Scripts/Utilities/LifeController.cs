using UnityEngine;

public class LifeController : MonoBehaviour
{
    public int maxLife = 100;
    private int currentLife;

    void Start()
    {
        currentLife = maxLife;
    }

    public void TakeDamage(int damage)
    {
        currentLife -= damage;
        if (currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}
