using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class Ice_Pick_Idle : StateMachineBehaviour
{

    private NavMeshAgent agent;
    private GameObject player;
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
            player = enemyData.player;
            agent = enemyData.agent;
        }

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
            originalSeeker = true;
            foreach(GameObject x in EnemyData.icePickEnemies)
            {
                if ((enemy.transform.position - x.transform.position).magnitude < enemyData.icePickRange)
                {
                    if (!x.GetComponent<Animator>().GetBehaviour<Ice_Pick_Idle>().foundEnemy)
                    {
                        x.GetComponent<Animator>().SetBool("Tracking", true);
                        if(!x.GetComponent<Animator>().GetBehaviour<Ice_Pick_Idle>().originalSeeker)
                             x.GetComponent<Animator>().SetFloat("IdleTime", 1.0f);
                        x.GetComponent<Animator>().GetBehaviour<Ice_Pick_Idle>().foundEnemy = true;
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
