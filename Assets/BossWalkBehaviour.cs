using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalkBehaviour : StateMachineBehaviour
{
    public float speed = 2.5f;

    Transform player;
    Rigidbody2D rigidBody;
    ToothBoss boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       rigidBody = animator.GetComponent<Rigidbody2D>();
       boss = animator.GetComponent<ToothBoss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        if(!boss.PlayerInAttackRange()) {
            Vector2 target = new Vector2(player.position.x, rigidBody.position.y);
            Vector2 newPos = Vector2.MoveTowards(rigidBody.position, target, speed * Time.fixedDeltaTime);
            rigidBody.MovePosition(newPos);
        }
        boss.Attack(animator);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
