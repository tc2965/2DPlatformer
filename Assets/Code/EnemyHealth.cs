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

    void Start()
    {
        maxHealth = 100;
        health = maxHealth;
        healthImage.SetActive(false);
    }

    void Update() 
    {
        healthBar.value = CalculateHealth();
        if (health < maxHealth)
        {
            healthImage.SetActive(true);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    float CalculateHealth() 
    {
        return health/maxHealth;
    }

    public void TakeDamage(int damage) 
    {
        health -= damage;
    }
}
