using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float collisionOffset = 0.05f;
    Rigidbody2D rb;
    Vector2 movementInput;
    [SerializeField] private ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollision = new List<RaycastHit2D>();

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){

    }

    private void FixedUpdate(){

        if (movementInput != Vector2.zero){
            bool success = TryMove(movementInput);
            if (!success){
                success = TryMove(new Vector2(movementInput.x, 0));
                if (!success){
                    success = TryMove(new Vector2(0,movementInput.y));
                }
            }
        }
    } 
    private bool TryMove(Vector2 direction){
        int Count = rb.Cast(direction,
                            movementFilter,
                            castCollision,
                            speed * Time.fixedDeltaTime + collisionOffset);
        if (Count == 0){
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
            return true;
        }
        else{
            return false;
        }
    }

    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get <Vector2>();
    }
}
