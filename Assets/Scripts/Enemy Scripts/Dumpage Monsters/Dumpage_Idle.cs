using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dumpage_Idle : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private GameObject enemy;
    private EnemyData enemyData;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        if (enemy.GetComponent<EnemyData>())
        {
            enemyData = enemy.GetComponent<EnemyData>();
            player = enemyData.player;
            agent = enemyData.agent;
        }
        enemy.GetComponent<MeshRenderer>().enabled = false;
        agent.isStopped = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetFloat("IdleTime") > 0.0f)
            animator.SetFloat("IdleTime", animator.GetFloat("IdleTime") - Time.deltaTime);
        if ((enemy.transform.position - player.transform.position).magnitude < enemyData.searchRadius)
        {
            animator.SetBool("Tracking", true);
        }
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
