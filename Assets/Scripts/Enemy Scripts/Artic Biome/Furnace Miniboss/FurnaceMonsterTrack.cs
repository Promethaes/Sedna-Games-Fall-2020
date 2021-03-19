using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FurnaceMonsterTrack : StateMachineBehaviour
{
    private GameObject[] players;
    private GameObject enemy;
    private EnemyData enemyData;
    private NavMeshAgent agent;
    private FurnaceMonster furnaceMonsterData;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        if (enemy.GetComponent<EnemyData>())
        {
            enemyData = enemy.GetComponent<EnemyData>();
            players = enemyData.getPlayers();
            agent = enemyData.getNavMeshAgent();
            furnaceMonsterData = enemy.GetComponent<FurnaceMonster>();
        }
        agent.isStopped = false;
        enemyData.enemySounds[(int)EnemySoundIndex.Movement].Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float shortestMag = 1000.0f;
        GameObject closestPlayer = null;
        foreach (GameObject player in players)
        {
            float lMag = (enemy.transform.position - player.transform.position).magnitude;
            if (lMag < shortestMag)
            {
                shortestMag = lMag;
                closestPlayer = player;
            }
        }

        if (shortestMag == 1000.0f)
        {
            agent.SetDestination(enemy.transform.position);
            animator.SetBool("tracking", false);
            animator.SetFloat("idleTime", 2.0f);
        }

        var mag = (enemy.transform.position - closestPlayer.transform.position).magnitude;
        agent.SetDestination(closestPlayer.transform.position);
        var p = new Vector3(closestPlayer.transform.position.x,enemy.transform.position.y,closestPlayer.transform.position.z);
        enemy.transform.LookAt(p);
        if (mag < 10)
        {
            animator.SetBool("tracking", false);
            if (animator.GetBool("fire") || animator.GetBool("attack1") || animator.GetBool("attack2")
                || animator.GetBool("attack3"))
                return;

            if (furnaceMonsterData.DecideAttack())
                animator.SetBool("fire", true);
            else
                animator.SetBool("attack1", true);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
