using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerTrigger : MonoBehaviour
{
    public GameObject EnemySpawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("on trigger enter");
        if (other.gameObject.CompareTag("Player")) {
            EnemySpawner.GetComponent<EnemySpawner>().SpawnEnemies();
        }
    }
}
