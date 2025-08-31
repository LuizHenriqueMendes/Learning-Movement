using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private bool tripleShoot = true;
    [SerializeField] private RawImage[] life;
    private int currentLives = 3;
    private SpriteRenderer targetSpriteRenderer;
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
                if (!tripleShoot)
                {
                    GameObject b = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
                    // GameObject b = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
                    b.GetComponent<Bullet>().SetDirection(lastDirection);
                    canShoot = shootTime;
                }
                else
                {                    
                    GameObject b1 = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
                    b1.GetComponent<Bullet>().SetDirection(lastDirection);

                    //Crio uma nova direção para o tiro triplo:
                    Vector2 dir2 = Quaternion.AngleAxis(30, Vector3.forward) * lastDirection;
                    GameObject b2 = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
                    b2.GetComponent<Bullet>().SetDirection(dir2);

                    Vector2 dir3 = Quaternion.AngleAxis(-30, Vector3.forward) * lastDirection;
                    GameObject b3 = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
                    b3.GetComponent<Bullet>().SetDirection(dir3);

                    canShoot = shootTime;
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Enemy")
        {
            Debug.Log("Ouch!");
            LoseLife();
        }
    }

    void LoseLife() //not working
    {
        if (currentLives > 0)
    {
        // acessa o último ocupado e desativa
        life[currentLives - 1].enabled = false;
        currentLives--;
    }

    if (currentLives <= 0)
    {        
        Destroy(gameObject);
    }
    }

}