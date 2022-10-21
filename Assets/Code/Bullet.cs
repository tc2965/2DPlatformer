using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float lifetime = 2.0f;

    void Awake () {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(10);
            Destroy(gameObject);
        }
    }
}