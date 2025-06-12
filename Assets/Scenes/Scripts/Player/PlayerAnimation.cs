using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerController controller;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        //Vector2 dir = controller.Direction;

        //animator.SetBool("isWalking", dir.magnitude > 0);
        //animator.SetFloat("DirX", dir.x);
        //animator.SetFloat("DirY", dir.y);
    }
}
