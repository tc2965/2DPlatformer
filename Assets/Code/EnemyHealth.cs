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

    void Start()
    {
        maxHealth = 100;
        health = maxHealth;
        healthImage.SetActive(false);
        _animator = GetComponent<Animator>();
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
            StartCoroutine(Die());
            Destroy(gameObject);
        }
    }

    IEnumerator Die() {
        _animator.SetTrigger("Die");
        yield return new WaitForSeconds(3);
    }
}
