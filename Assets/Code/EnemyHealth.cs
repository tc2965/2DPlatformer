using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour
{
    public float health; 
    public float maxHealth; 
    public GameObject healthImage; 
    public Slider healthBar;
    Animator _animator;
    // private BunnyExamplePublisher enemyDeathPublisher;

    void Start()
    {
        maxHealth = 100;
        health = maxHealth;
        healthImage.SetActive(false);
        _animator = GetComponent<Animator>();
        // GameObject publisher = GameObject.FindGameObjectWithTag("Publisher");
        // if (publisher != null) {
        //     enemyDeathPublisher = publisher.GetComponent<BunnyExamplePublisher>();
        // } else {
        //     print("no publisher found in enemy health");
        // }
    }

    void Update() 
    {
        healthBar.value = CalculateHealth();
        if (health < maxHealth)
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
