using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{

    
    [SerializeField] private float speed;
    [SerializeField] private float jump;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private bool canJump;
    private bool doubleJump;

    private float move;
    private Rigidbody2D rb;
    

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){

        move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if(isGrounded()){
            doubleJump = false;
        }

        if(Input.GetButtonDown("Jump")){

            if(isGrounded() || !doubleJump){

                rb.velocity = new Vector2(move * speed, jump);

                if(!isGrounded()){
                    doubleJump = true;
                }
            }
        }
    }

    private bool isGrounded(){
        canJump = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.1f, 1.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
        return canJump;
    }

}





