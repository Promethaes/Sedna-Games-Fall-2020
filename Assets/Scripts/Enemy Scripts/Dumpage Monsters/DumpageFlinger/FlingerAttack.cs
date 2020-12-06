using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlingerAttack : StateMachineBehaviour
{
    private GameObject[] players;
    private GameObject enemy;
    private EnemyData enemyData;
    private RangedEnemyData rangedEnemyData;
    private NavMeshAgent agent;

    bool _shouldFling = true;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        if (enemy.GetComponent<EnemyData>() && enemy.GetComponent<RangedEnemyData>())
        {
            enemyData = enemy.GetComponent<EnemyData>();
            players = enemyData.getPlayers();
            agent = enemyData.getNavMeshAgent();
            rangedEnemyData = enemy.GetComponent<RangedEnemyData>();
        }
        _shouldFling = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //TODO: Animation; heavy sludge attacks(?)
        if (stateInfo.normalizedTime >= 0.5f && _shouldFling)
        {
            rangedEnemyData.Fling();
            _shouldFling = false;
            enemyData.enemySounds[(int)EnemySoundIndex.Attack].Play();
        }

        animator.SetBool("attack", false);
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
