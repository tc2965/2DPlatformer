using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float distance = 3; 
    public float speedEnemy = 0.25f;
    private GameObject player; 
    private Transform playerPosition; 
    private Vector2 currentPosition; 
    private bool facingRight;
    Animator _animator;

    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player) {
            print("can't find player");
        }
        _animator = GetComponent<Animator>();
        playerPosition = player.GetComponent<Transform>();
        currentPosition = GetComponent<Transform>().position;
    }

    void Update() 
    {
        if (Vector2.Distance(transform.position, playerPosition.position) < distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, speedEnemy * Time.deltaTime);
            _animator.SetFloat("Speed", Mathf.Abs(speedEnemy));
            float playerToEnemy = transform.position.x - playerPosition.position.x;
            if (playerToEnemy < 0 && facingRight) {
                facingRight = !facingRight;
                transform.Rotate(new Vector3(0, 180, 0));
            } else if (playerToEnemy > 0 && !facingRight) {
                facingRight = !facingRight;
                transform.Rotate(new Vector3(0, 180, 0));
            }
        }
    }
}

