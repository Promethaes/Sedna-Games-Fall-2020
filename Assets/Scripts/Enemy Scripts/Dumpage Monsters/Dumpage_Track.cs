﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dumpage_Track : StateMachineBehaviour
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
            players = enemyData.getPlayers();
            agent = enemyData.getNavMeshAgent();
        }
        agent.isStopped = false;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (GameObject player in players)
        {
            if ((enemy.transform.position - player.transform.position).magnitude < enemyData.searchRadius + 5)
            {
                agent.SetDestination(player.transform.position);
            }
            else
            {
                agent.SetDestination(enemy.transform.position);
                animator.SetBool("tracking", false);
                animator.SetFloat("idleTime", 2.0f);
            }
            if ((enemy.transform.position - player.transform.position).magnitude < 3)
            {
                animator.SetBool("attack",true);
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
