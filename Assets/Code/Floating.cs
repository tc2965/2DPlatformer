using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    private float speed = 1.0f;
    private float yStart;
    private Vector3 _position;

    void Start()
    {
        yStart = transform.position.y;
        _position = transform.position;
    }

    void Update()
    {
        _position.y = yStart + 0.5f * Mathf.Sin(speed * Time.fixedTime);
        transform.position = _position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerCode>().IncrementBullets();
            Destroy(gameObject);
        }
    }
}
