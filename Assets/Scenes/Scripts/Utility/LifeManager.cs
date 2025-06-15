using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private AnimationController animationController;
    [SerializeField] private string prefix;

    void Start()
    {
        currentHealth = maxHealth;
        animationController = GetComponent<AnimationController>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (animationController != null)
            animationController.TriggerHit(prefix);

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        if (animationController != null)
            animationController.TriggerDeath(prefix);

        Destroy(gameObject, 0.5f);
    }
}
