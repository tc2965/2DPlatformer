using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Events;

public class EnemyDeath
{
    public bool died = false;
}

public class NextLevel : MonoBehaviour
{
    public Action<BunnyBrokerMessage<EnemyDeath>> action;
    private int enemiesDead = 0;
    private GameManager gameManager;

    private void Start()
    {
        GameObject gameManagement = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManagement) {
            gameManager = gameManagement.GetComponent<GameManager>();
        } else {
            print("no game manager found in enemy health");
        }

        action = anEnemyDied;
        // BunnyEventManager.Instance.RegisterEvent("EnemyDied", this);
        BunnyEventManager.Instance.OnEventRaised<EnemyDeath>("EnemyDied", action);
    }
    
    private void anEnemyDied(BunnyBrokerMessage<EnemyDeath> msg) {
        enemiesDead++;
        print(enemiesDead);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        print("in trigger");
        print(enemiesDead);
        if (other.gameObject.CompareTag("Player") && enemiesDead > 3) {
            gameManager.LoadNextLevel();
        }
    }
}
