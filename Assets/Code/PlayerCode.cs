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

    public bool grounded = false;
    public Slider healthBar;

    public LayerMask groundLayer;
    public LayerMask enemyLayers;

    Rigidbody2D _rigidbody; //in inspector: make gravity scale 5, freeze rotation on z axis
    Animator _animator;
    float xSpeed =  0;

    public bool paused = false;



    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

    }

    private void Update() {
        if(!paused) 
        {
            xSpeed = Input.GetAxisRaw("Horizontal") * speed;
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
                _animator.SetTrigger("Shoot");
                Attack(25);
            } else {
                _animator.SetFloat("Speed", Mathf.Abs(xSpeed));
            }
        } 
        else 
        {
            Time.timeScale = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        print("hit something");
        if (other.gameObject.CompareTag("Enemy")) {
            print("hit enemy");
        }
    }

    private void TakeDamage() {
        health -= 10;
        _animator.SetTrigger("Damaged");
        UpdateHealthBar();
    }

    void UpdateHealthBar() {
        healthBar.value = health/maxHealth;
    }

    void Attack(int damage) {
        print("in attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        print(hitEnemies);
        foreach(Collider2D enemy in hitEnemies) {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
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

}
