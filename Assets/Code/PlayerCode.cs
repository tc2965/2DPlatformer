using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCode : MonoBehaviour
{
    public int speed = 5;
    public int jumpforce = 800;

    public Transform feetTrans;
    public bool grounded = false;

    public LayerMask groundLayer;

    Rigidbody2D _rigidbody; //in inspector: make gravity scale 5, freeze rotation on z axis
    Animator _animator;
    float xSpeed =  0;

    public bool paused = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        xSpeed = Input.GetAxisRaw("Horizontal") * speed;
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y); 

        _animator.SetFloat("Speed", Mathf.Abs(xSpeed));
    }

    private void Update() {

        if(!paused) 
        {
            grounded = Physics2D.OverlapCircle(feetTrans.position, .3f, groundLayer);
            if(Input.GetButtonDown("Jump") && grounded) 
            {
                _rigidbody.AddForce(new Vector2(0, jumpforce));
            }
        } 
        else 
        {
            Time.timeScale = 0;
        }
    }
}
