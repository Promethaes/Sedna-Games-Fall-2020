using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class Robo_Pick_Idle : StateMachineBehaviour
{
    private GameObject[] players;
    private GameObject enemy;
    private EnemyData enemyData;
    private NavMeshAgent agent;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        if (enemy.GetComponent<EnemyData>())
        {
            enemyData = enemy.GetComponent<EnemyData>();
            agent = enemyData.getNavMeshAgent();
            players = enemyData.getPlayers();
        }
        agent.isStopped = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetFloat("idleTime") > 0.0f)
            animator.SetFloat("idleTime", animator.GetFloat("idleTime") - Time.deltaTime);

        
        foreach(GameObject player in players)
        {
            if ((enemy.transform.position - player.transform.position).magnitude < enemyData.searchRadius)
            {
                animator.SetBool("tracking", true);
                break;
            }
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
