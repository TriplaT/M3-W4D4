using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Weapon weapon;
    public float fireRate = 2f;
    private float fireCooldown = 0f;

    private AnimationController animationController;

    void Start()
    {
        fireCooldown = fireRate;
        animationController = GetComponent<AnimationController>();
    }

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            weapon.Shoot();
            animationController.TriggerAttack("enemy_");
            fireCooldown = fireRate;
        }
    }
}
