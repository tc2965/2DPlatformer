using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroductionMovement : MonoBehaviour
{
    Animator _animator;
    private float walkspeed = 3.0f;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        _animator.SetFloat("Speed", walkspeed);
        transform.Translate(Vector2.right * Time.deltaTime * walkspeed);
        if (transform.position.y < -100) {
            SceneManager.LoadScene("Level1");
        }
    }
}
