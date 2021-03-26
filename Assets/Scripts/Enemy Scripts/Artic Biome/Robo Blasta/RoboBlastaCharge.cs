using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RoboBlastaCharge : StateMachineBehaviour
{
    private GameObject[] players;
    private GameObject enemy;
    private EnemyData enemyData;
    private Shooter shooterData;
    private NavMeshAgent agent;

    bool _shouldShoot = true;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        if (enemy.GetComponent<EnemyData>() && enemy.GetComponent<Shooter>())
        {
            enemyData = enemy.GetComponent<EnemyData>();
            players = enemyData.getPlayers();
            agent = enemyData.getNavMeshAgent();
            shooterData = enemy.GetComponent<Shooter>();
        }
        _shouldShoot = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //TODO: Animation; heavy sludge attacks(?)
        if (stateInfo.normalizedTime >= 0.5f && _shouldShoot)
        {
            shooterData.Charge();

            _shouldShoot = false;
            enemyData.enemySounds[(int)EnemySoundIndex.Attack].Play();
        }
        animator.SetBool("attack", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

}
