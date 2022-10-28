using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerCode : MonoBehaviour
{
    public float speed = 5.0f;
    public int jumpforce = 25;
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
    GameManager gameManager;
    float xSpeed =  0;
    private bool facingRight;
    private int numberOfBullets = 0;
    [SerializeField] private TextMeshProUGUI ammoCountText;

    void Start()
    {
        facingRight = true;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        GameObject gameManagement = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManagement) {
            gameManager = gameManagement.GetComponent<GameManager>();
        } else {
            print("no game manager found in enemy health");
        }
        print(gameObject.name);
        BunnyEventManager.Instance.RegisterEvent("PlayerTakeDamage", this);
        Action<BunnyBrokerMessage<float>> takeDamageCallback = BunnyTakeDamage;
        BunnyEventManager.Instance.OnEventRaised<float>("PlayerTakeDamage", takeDamageCallback);
        UpdateAmmo();
    }
    

    // <-- NEEDED FOR INPUT ------------------------------>
    public void Move(InputAction.CallbackContext context) {
        xSpeed = context.ReadValue<Vector2>().x;
        if (xSpeed > 0 && !facingRight) {
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        } else if (xSpeed < 0 && facingRight) {
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    public void Jump(InputAction.CallbackContext context) {
        if (context.performed && grounded) 
        {
            _rigidbody.AddForce(new Vector2(0, jumpforce));
        }
    }

    public void Punch(InputAction.CallbackContext context) {
        if (context.performed) 
        {
            _animator.SetTrigger("Punch");
            Attack(10);
        }
    }

    public void Kick(InputAction.CallbackContext context) {
        if (context.performed) 
        {
            _animator.SetTrigger("Kick");
            Attack(15);
        }
    }

    public void SpeedUp(InputAction.CallbackContext context) {
        if (context.performed && context.ReadValueAsButton()) {
            speed = 10.0f;
        } else {
            speed = 5.0f;
        }
    }

    public void Shoot(InputAction.CallbackContext context) {
        if (context.performed && numberOfBullets > 0) {
            numberOfBullets--;
            _animator.SetTrigger("Shoot");
            GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(attackPoint.right * 5000.0f);
            Attack(25);
            UpdateAmmo();
        }
    }
    // <-------------------------------------------------->

    void FixedUpdate()
    {
        if (transform.position.y < -75) {
            health = 0;
            UpdateHealthBar();
        }
        _rigidbody.velocity = new Vector2(xSpeed * speed, _rigidbody.velocity.y);
        _animator.SetFloat("Speed", Mathf.Abs(xSpeed));
        grounded = Physics2D.OverlapCircle(feetTrans.position, .3f, groundLayer);
    }

    private void Update() {
        if(!paused) 
        {
            Time.timeScale = 1;
            // if (Input.GetKey(KeyCode.LeftShift)) {
            //     speed = 10;
            // } else {
            //     speed = 5;
            // }

        } 
        else 
        {
            Time.timeScale = 0;
        }
    }

    public void TakeDamage(float damage = 3.0f) {
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
        UpdateAmmo();
    }

    public void UpdateAmmo() {
        if(ammoCountText == null)
            return;
        ammoCountText.text = "ammo: " + numberOfBullets.ToString();
    }

    public void BunnyTakeDamage(BunnyBrokerMessage<float> data)
    {
        print("Player is taking damage!");
        TakeDamage(data.payload);
    }
}
