using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jump;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;    
    [SerializeField] GameObject landMine;
    [SerializeField] GameObject bullet;
    [SerializeField] float landMineTime;
    [SerializeField] float shootTime;
    [SerializeField] Transform bulletSpawnPoint;

    private bool canJump;
    private bool doubleJump;

    private Rigidbody2D rb;
    private float move;
    private float moveUp;
    private float canLandMine;
    private float canShoot;
    private float shootSpeedMultiplier = 2f;
    private Vector2 lastDirection = Vector2.right;
    private Vector2 direction;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");
        moveUp = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(move * speed, moveUp * speed);

        // Atualiza a direção
        Vector2 currentDirection = new Vector2(move, moveUp);
        if (currentDirection.sqrMagnitude > 0.01f)
        {
            lastDirection = currentDirection.normalized;
        }

        // Calcula o ângulo da mira
        float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // 🔥 Flip horizontal no eixo Y (invertendo X no localScale)
        if (move < -0.01f)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        else if (move > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // LandMine
        canLandMine -= (Time.deltaTime * shootSpeedMultiplier);
        if (canLandMine <= 0)
        {
            if (Input.GetKeyDown("e"))
            {
                Instantiate(landMine, transform.position, Quaternion.identity);
                canLandMine = landMineTime;
            }
        }

        // Tiro
        canShoot -= (Time.deltaTime * shootSpeedMultiplier);
        if (canShoot <= 0)
        {
            if (Input.GetKeyDown("space"))
            {
                GameObject b = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
                b.GetComponent<Bullet>().SetDirection(lastDirection);
                canShoot = shootTime;
            }
        }
    }

        
    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Enemy"){
            Destroy(gameObject);    
        }
    }
}


// if(isGrounded())
        // {
        //     doubleJump = false;
        // }

        // if(Input.GetButtonDown("Jump"))
        // {
        //     if(isGrounded() || !doubleJump)
        //     {
        //         rb.velocity = new Vector2(rb.velocity.x, jump);
        //         if(!isGrounded())
        //         {
        //             doubleJump = true;
        //         }
        //     }
        // }

    // private bool isGrounded()
    // {
    //     canJump = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.1f, 1.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    //     return canJump;
    // }