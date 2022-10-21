using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCode : MonoBehaviour
{
    public int speed = 5;
    public int jumpforce = 800;
    public float health = 100;
    public float maxHealth = 100;

    public Transform feetTrans;
    public Transform attackPoint;
    public float attackRange = 0.6f;
    public GameObject bulletPrefab;

    public bool grounded = false;
    public Slider healthBar;

    public LayerMask groundLayer;
    public LayerMask enemyLayers;

    public bool paused = false;

    Rigidbody2D _rigidbody; //in inspector: make gravity scale 5, freeze rotation on z axis
    Animator _animator;
    SpriteRenderer _renderer;
    GameManager gameManager;
    float xSpeed =  0;
    private bool facingRight;
    private int numberOfBullets = 0;

    void Start()
    {
        facingRight = true;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();

        GameObject gameManagement = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManagement) {
            gameManager = gameManagement.GetComponent<GameManager>();
        } else {
            print("no game manager found in enemy health");
        }
    }

    void FixedUpdate()
    {
        if (transform.position.y < -100) {
            health = 0;
            UpdateHealthBar();
        }
    }

    private void Update() {
        if(!paused) 
        {
            Time.timeScale = 1;
            if (Input.GetKey(KeyCode.LeftShift)) {
                speed = 10;
            } else {
                speed = 5;
            }
            xSpeed = Input.GetAxisRaw("Horizontal") * speed;
            if (xSpeed > 0 && !facingRight) {
                facingRight = !facingRight;
                transform.Rotate(new Vector3(0, 180, 0));
            } else if (xSpeed < 0 && facingRight) {
                facingRight = !facingRight;
                transform.Rotate(new Vector3(0, 180, 0));
            }
            _animator.SetFloat("Speed", Mathf.Abs(xSpeed));
            _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y); 
            // if (xSpeed > 0.01) {
            //     _animator.ResetTrigger("Punch");
            //     _animator.ResetTrigger("Jump");
            // }

            grounded = Physics2D.OverlapCircle(feetTrans.position, .3f, groundLayer);
            if (Input.GetButtonDown("Jump") && grounded) 
            {
                _rigidbody.AddForce(new Vector2(0, jumpforce));
                _animator.SetTrigger("Jump");
            } else if (Input.GetKeyDown(KeyCode.Q)) {
                _animator.SetTrigger("Punch");
                Attack(10);
            } else if (Input.GetKeyDown(KeyCode.W)) {
                _animator.SetTrigger("Kick");
                Attack(15);
            } else if (Input.GetKeyDown(KeyCode.E)) {
                if (numberOfBullets > 0) {
                    numberOfBullets--;
                    _animator.SetTrigger("Shoot");
                    GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, transform.rotation);
                    bullet.GetComponent<Rigidbody2D>().AddForce(attackPoint.right * 5000.0f);
                    Attack(25);
                }
            }
        } 
        else 
        {
            Time.timeScale = 0;
        }
    }

    public void TakeDamage(int damage = 3) {
        health -= damage;
        _animator.SetTrigger("Damaged");
        UpdateHealthBar();
    }

    void UpdateHealthBar() {
        healthBar.value = health/maxHealth;
        if (health <= 0) {
            gameManager.ShowDeathScreen();
        }
    }

    void Attack(int damage) {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies) {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
            damage -= 5;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) {
            return;
        } else {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

    public void IncrementBullets() {
        numberOfBullets += 10;
    }
}
