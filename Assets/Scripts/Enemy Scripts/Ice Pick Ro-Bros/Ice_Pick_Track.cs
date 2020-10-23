using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ice_Pick_Track : StateMachineBehaviour
{
    private GameObject player;
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
            player = enemyData.player;
            agent = enemyData.agent;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((enemy.transform.position - player.transform.position).magnitude < enemyData.searchRadius + 5)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.SetDestination(enemy.transform.position);
            animator.SetBool("Tracking", false);
            animator.SetFloat("IdleTime", 2.0f);
        }
        enemyData._animationDuration -= Time.deltaTime;
        Combo();
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

    void Combo()
    {
        if (enemyData._animationDuration < 0.0f)
        {
            enemyData._animationDuration = enemyData._animationDelay[enemyData._comboCounter];
            //TODO: Animation; heavy sludge attacks(?)
            RaycastHit target;
            if (Physics.Raycast(enemy.transform.position, enemy.transform.forward, out target, enemyData._range[enemyData._comboCounter]) && target.transform.tag == "Player")
            {
                PlayerBackend foe = target.collider.GetComponentInParent<PlayerBackend>();
                foe.hp -= enemyData._damageValues[enemyData._comboCounter];
                Debug.Log(foe.hp);
            }
            enemyData._comboCounter++;
            if (enemyData._comboCounter > 2)
                enemyData._comboCounter = 0;
        }
    }
}
