using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class Ice_Pick_Idle : StateMachineBehaviour
{

    private NavMeshAgent agent;
    private GameObject[] players;
    private GameObject enemy;
    private EnemyData enemyData;

    public bool foundEnemy = false;
    public bool originalSeeker = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foundEnemy = false;
        enemy = animator.gameObject;
        if (enemy.GetComponent<EnemyData>())
        {
            enemyData = enemy.GetComponent<EnemyData>();
            players = enemyData.getPlayers();
            agent = enemyData.getNavMeshAgent();
        }

        agent.isStopped = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetFloat("idleTime") > 0.0f)
            animator.SetFloat("idleTime", animator.GetFloat("idleTime") - Time.deltaTime);
        foreach (GameObject player in players)
        {
            if ((enemy.transform.position - player.transform.position).magnitude < enemyData.searchRadius)
            {
                agent.SetDestination(player.transform.position);
            }
            else
            {
                agent.SetDestination(enemy.transform.position);
                animator.SetBool("tracking", false);
            }
            if ((enemy.transform.position - player.transform.position).magnitude < enemyData.searchRadius)
            {
                animator.SetBool("tracking", true);
                originalSeeker = true;
                    if ((enemy.transform.position - player.transform.position).magnitude < enemyData.searchRadius)
                    {
                        if (!player.GetComponent<Animator>().GetBehaviour<Ice_Pick_Idle>().foundEnemy)
                        {
                            player.GetComponent<Animator>().SetBool("tracking", true);
                            if (!player.GetComponent<Animator>().GetBehaviour<Ice_Pick_Idle>().originalSeeker)
                                player.GetComponent<Animator>().SetFloat("idleTime", 1.0f);
                            player.GetComponent<Animator>().GetBehaviour<Ice_Pick_Idle>().foundEnemy = true;
                        }
                        //x join the hunt
                    }
            }
        }
            
            

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        originalSeeker = false;
        agent.isStopped = false;
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
