using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateMovementAnimation(string prefix, Vector2 movement)
    {
        animator.SetFloat(prefix + "Horizontal", movement.x);
        animator.SetFloat(prefix + "Vertical", movement.y);
        animator.SetFloat(prefix + "Speed", movement.sqrMagnitude);
    }

    public void TriggerAttack(string prefix)
    {
        animator.SetTrigger(prefix + "Attack");
    }

    public void TriggerDeath(string prefix)
    {
        animator.SetTrigger(prefix + "Death");
    }

    public void TriggerHit(string prefix)
    {
        animator.SetTrigger(prefix + "Hit");
    }
}
