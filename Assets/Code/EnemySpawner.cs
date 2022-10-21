using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;

    float spawnTimer = 2.0f; 
    int enemiesSpawned = 0;
    List<GameObject> enemiesList;
    Vector3 positionOffset;

    private void Start()
    {
        enemiesList = new List<GameObject>();
        enemiesList.Add(enemy1Prefab);
        enemiesList.Add(enemy2Prefab);
        enemiesList.Add(enemy3Prefab);

        positionOffset = new Vector3(1.0f, 0.0f, 0.0f);
    }

    public void SpawnEnemies()
    {
        print(positionOffset);
        print("spawning enemies now");
        StartCoroutine(spawnEnemies());
    }
    
    private IEnumerator spawnEnemies() {
        while (enemiesSpawned < 3) 
        {
            Instantiate(enemiesList[Random.Range(0, 3)], transform.position + positionOffset ,transform.rotation);
            Instantiate(enemiesList[Random.Range(0, 3)], transform.position - positionOffset ,transform.rotation);
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
