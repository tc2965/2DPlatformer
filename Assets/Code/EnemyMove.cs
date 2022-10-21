using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private float distance = 15; 
    public float speedEnemy = 0.25f;
    public Transform attackPosition;
    public float attackRange = 0.6f;

    private GameObject player; 
    private Transform playerPosition; 
    private Vector2 currentPosition; 
    private bool facingRight;
    Animator _animator;

    void Start() 
    {
        speedEnemy = Random.Range(0.25f, 1.0f);
        facingRight = false;
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
        float playerToEnemyy = Mathf.Abs(transform.position.y - playerPosition.position.y);
        if (playerToEnemyy < 1 && 
            Vector2.Distance(transform.position, playerPosition.position) < distance) {

            transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, speedEnemy * Time.deltaTime);
            _animator.SetFloat("Speed", Mathf.Abs(speedEnemy));
            float playerToEnemyx = transform.position.x - playerPosition.position.x;
            if (playerToEnemyx < 0 && facingRight) {
                facingRight = !facingRight;
                transform.Rotate(new Vector3(0, 180, 0));
            } else if (playerToEnemyx > 0 && !facingRight) {
                facingRight = !facingRight;
                transform.Rotate(new Vector3(0, 180, 0));
            }
        } else {
            _animator.SetFloat("Speed", 0.0f);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerCode>().TakeDamage(Random.Range(0.05f, 0.5f));
        }
    }
}
