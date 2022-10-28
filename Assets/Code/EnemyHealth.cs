using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour
{
    public float health; 
    public float maxHealth = 100; 
    public GameObject healthImage; 
    public Slider healthBar;
    Animator _animator;
    ToothBoss boss;
    // private BunnyExamplePublisher enemyDeathPublisher;

    void Start()
    {
        health = maxHealth;
        if(healthImage != null)
            healthImage.SetActive(false);
        _animator = GetComponent<Animator>();
        boss = GetComponent<ToothBoss>();
        // GameObject publisher = GameObject.FindGameObjectWithTag("Publisher");
        // if (publisher != null) {
        //     enemyDeathPublisher = publisher.GetComponent<BunnyExamplePublisher>();
        // } else {
        //     print("no publisher found in enemy health");
        // }
    }

    void Update() 
    {
        if(healthBar != null)
            healthBar.value = CalculateHealth();
        if (health < maxHealth && healthImage != null)
        {
            healthImage.SetActive(true);
        }
    }

    float CalculateHealth() 
    {
        return health/maxHealth;
    }

    public void TakeDamage(int damage) 
    {
        if(boss)
            boss.TakeDamage((float) damage);
        health -= damage;
        if (health <= 0)
        {
            _animator.SetTrigger("Die");
            StartCoroutine(Die());
            EnemyDied();
            Destroy(gameObject);
        }
    }

    IEnumerator Die() {
        yield return new WaitForSeconds(3);
    }

    public void EnemyDied() 
    {
        BunnyBrokerMessage<EnemyDeath> enemyDied = new BunnyBrokerMessage<EnemyDeath>(
            new EnemyDeath() {
                died = true
            }, 
            this
        );
        BunnyEventManager.Instance.Fire<EnemyDeath>("EnemyDied", enemyDied);
    }
}
