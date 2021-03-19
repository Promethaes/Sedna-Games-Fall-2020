using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FurnaceMonsterIdle : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private GameObject[] players;
    private GameObject enemy;
    private EnemyData enemyData;
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
        enemyData.enemySounds[(int)EnemySoundIndex.Spawn].Play();
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
}
