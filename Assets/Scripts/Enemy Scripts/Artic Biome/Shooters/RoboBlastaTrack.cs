using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoboBlastaTrack : StateMachineBehaviour
{
    private GameObject[] players;
    private GameObject enemy;
    private EnemyData enemyData;
    private NavMeshAgent agent;
    private Shooter shooterData;
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
        agent.isStopped = false;
        enemyData.enemySounds[(int)EnemySoundIndex.Spawn].Play();


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


        var mag = (enemy.transform.position - closestPlayer.transform.position).magnitude;
        agent.SetDestination(closestPlayer.transform.position);
        if (mag <= 100.0f)
        {
            enemy.transform.LookAt(closestPlayer.transform);
            animator.SetBool("attack", true);
        }
        else
        {
            animator.SetBool("tracking", false);
            animator.SetFloat("idleTime", 2.0f);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
