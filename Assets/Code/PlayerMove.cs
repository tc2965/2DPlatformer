// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;


// public class PlayerMove : MonoBehaviour
// {
//     public int jumpforce = 800;
//     private Rigidbody2D _rigidbody;
//     PlayerInputActions playerInputActions;

//     float xSpeed = 0;

//     private void Awake()
//     {
//         _rigidbody = GetComponent<Rigidbody2D>();
//         if (_rigidbody == null) {
//             print("RIGIDBODY NOT FOUND");
//         }
//         playerInputActions = new PlayerInputActions();
//         playerInputActions.Player.Enable();
//         if (playerInputActions == null) {
//             print("INPUT NOT FOUND");
//         }
//         playerInputActions.Player.Jump.performed += Jump;
//         playerInputActions.Player.Movement.performed += Movement;
//     }

//     public void Jump(InputAction.CallbackContext context) {
//         if (context.performed) {
//             // print("jump was called IN PLAYEer move");
//             _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 5.0f);
//         }
//         if (context.canceled) {
//             _rigidbody.velocity = new Vector2(0, 0);
//         }
//     }

//     private void Update()
//     {
//         _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
//     }

//     public void Movement(InputAction.CallbackContext context) {
//         // print("player movement");
//         // if (context.performed) {
//         //     xSpeed = 5.0f;
//         // } else if (context.canceled) {
//         //     print("when does this cancel");
//         //     xSpeed = 0.0f;
//         // }
//         xSpeed = context.ReadValue<Vector2>().x;
//     }

//     private void Jump() {

//     }
// }
