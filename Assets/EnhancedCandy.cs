using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancedCandy : MonoBehaviour
{
    private float speed = 1.0f;
    private float yStart;
    private Vector3 _position;
    SpriteRenderer renderer;
    bool debounce = true;

    void Start()
    {
        yStart = transform.position.y;
        _position = transform.position;
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _position.y = yStart + 0.5f * Mathf.Sin(speed * Time.fixedTime);
        transform.position = _position;
    }

    public IEnumerator CooldownHandler()
    {
        renderer.enabled = false;
        debounce = false;
        yield return new WaitForSeconds(10);
        renderer.enabled = true;
        debounce = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && debounce) {
            other.gameObject.GetComponent<PlayerCode>().IncrementBullets();
            other.gameObject.GetComponent<PlayerCode>().TakeDamage(-10);
            StartCoroutine(CooldownHandler());
        }
    }
}