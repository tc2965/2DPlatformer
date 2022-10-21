using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour
{
    public Action<BunnyBrokerMessage<EnemyDeath>> action;
    public GameObject bridge;
    private int leftSideEnemies = 8;

    private int enemiesDead = 0;

    private void Awake()
    {
        action = anEnemyDied;
        BunnyEventManager.Instance.RegisterEvent("EnemyDied", this);
        BunnyEventManager.Instance.OnEventRaised<EnemyDeath>("EnemyDied", action);
    }

    private void anEnemyDied(BunnyBrokerMessage<EnemyDeath> msg) {
        enemiesDead++;
        print(enemiesDead);
        if (enemiesDead == leftSideEnemies) {
            bridge.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bridge.SetActive(false);
    }

}
