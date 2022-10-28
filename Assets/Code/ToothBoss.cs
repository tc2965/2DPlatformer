using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState {
    IDLE,
    ENRAGED,
    ATTACK,
    CHASE
}

public class ToothBoss : MonoBehaviour
{
    Rigidbody2D _rigidbody; //in inspector: make gravity scale 5, freeze rotation on z axis
    Animator _animator;
    public BossState CurrentState;
    public LayerMask AttackMask;

    public Vector3 attackOffset;
    public int attackDamage = 10;
    public float maxAttackRange = 10;
    public float attackCooldown = 1;
    private float attackTimer = 0;
    public float Health = 100;

    public bool isFlipped = false;
    public Transform player;
    UIBossHealthBar bossHealthBar;

    private void Awake()
    {
        bossHealthBar = FindObjectOfType<UIBossHealthBar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossHealthBar.SetBossMaxHealth((int) Health);
        bossHealthBar.SetBossName("Dentist");
    }

    // Update is called once per frame
    void Update()
    {
        // _rigidbody.velocity = new Vector2(_rigidbody.velocity.x + 0.1, _rigidbody.velocity.y);
        _animator.SetFloat("Speed", Mathf.Abs(0.15f));
        attackTimer -= Time.deltaTime;
        BunnyEventManager.Instance.Fire<int>("BossOnDamage", new BunnyBrokerMessage<int>(
            (int) Health,
            this
        ));
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        BunnyEventManager.Instance.Fire<int>("BossOnDamage", new BunnyBrokerMessage<int>(
            (int) Health,
            this
        ));
    }

    public bool PlayerInAttackRange()
    {
        if(Vector2.Distance(player.position, _rigidbody.position) <= maxAttackRange)
        {
            return true;
        }
        return false;
    }

    public void Attack(Animator animator) {
        print("Player in range?" + PlayerInAttackRange());
        print("Player  attack timer <= 0" + (bool)(attackTimer <= 0));
        if(attackTimer <= 0 && PlayerInAttackRange())
        {
            animator.SetTrigger("AttackRequest");
            attackTimer = 1;

            float calculatedAttackDMG = attackDamage;

            Vector3 pos = transform.position;
            pos += transform.right * attackOffset.x;
            pos += transform.up * attackOffset.y;
            Collider2D collInfo = Physics2D.OverlapCircle(pos, maxAttackRange, AttackMask);
            if(collInfo != null)
            {
                BunnyEventManager.Instance.Fire<float>("PlayerTakeDamage", new BunnyBrokerMessage<float>(calculatedAttackDMG, this));
            }
        }
    }
}