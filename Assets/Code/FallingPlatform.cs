using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall() {
        yield return new WaitForSeconds(.1f);
        GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
