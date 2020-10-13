using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrol : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private GameObject enemy;
    private EnemyData enemyData;
    private GameObject player;
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
        agent.isStopped = false;
        Debug.Log("AI patrolling");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((enemy.transform.position - player.transform.position).magnitude < enemyData.searchRadius)
            animator.SetBool("tracking",true);
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    if (enemyData.currentPatrolGoal < enemyData.destinations.Count - 1)
                    {
                        enemyData.currentPatrolGoal++;
                    }
                    else
                    {
                        enemyData.currentPatrolGoal = 0;
                    }
                    agent.SetDestination(enemyData.destinations[enemyData.currentPatrolGoal].position);
                    Debug.Log(enemyData.destinations[enemyData.currentPatrolGoal].position.x + " " + enemyData.destinations[enemyData.currentPatrolGoal].position.y + " " + enemyData.destinations[enemyData.currentPatrolGoal].position.z);
                    Debug.Log("AI destination set");
                }
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
