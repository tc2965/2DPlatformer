using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothBoss : MonoBehaviour
{
    Rigidbody2D _rigidbody; //in inspector: make gravity scale 5, freeze rotation on z axis
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        StartCoroutine(goThroughAnims());
    }

    // Update is called once per frame
    void Update()
    {
        // _rigidbody.velocity = new Vector2(_rigidbody.velocity.x + 0.1, _rigidbody.velocity.y);
        _animator.SetFloat("Speed", Mathf.Abs(0.15f));
        
    }

    IEnumerator goThroughAnims() {
        _animator.SetFloat("Speed", Mathf.Abs(0.15f));
        yield return new WaitForSeconds(1);
        _animator.SetFloat("Speed", 0);
        yield return new WaitForSeconds(1);
        _animator.SetTrigger("Eat");
        yield return new WaitForSeconds(1);
        _animator.SetTrigger("Damaged");
        yield return new WaitForSeconds(1);
        _animator.SetTrigger("Rage");
        yield return new WaitForSeconds(1);
    }
}
