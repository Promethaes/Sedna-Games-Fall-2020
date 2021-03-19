using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FurnaceMonsterAttack : StateMachineBehaviour
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
        enemyData.hitbox.SetActive(true);
        enemyData.enemySounds[(int)EnemySoundIndex.Attack].Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //TODO: Animation; heavy sludge attacks(?)
        if (stateInfo.normalizedTime >= 1.0f)
        {
            if (stateInfo.IsName("Armature|Punch 1"))
            {
                furnaceMonsterData.ActivateClaw();
                animator.SetBool("attack1", false);
                animator.SetBool("attack2", true);
            }
            else if (stateInfo.IsName("Armature|Punch 2"))
            {
                furnaceMonsterData.ActivateClaw();
                animator.SetBool("attack2", false);
                animator.SetBool("attack3", true);
            }
            else if (stateInfo.IsName("Armature|Punch 3"))
            {
                furnaceMonsterData.ActivateClaw();
                animator.SetBool("attack3", false);
            }
            else if (stateInfo.IsName("Armature|Fire"))
            {
                furnaceMonsterData.ActivateFlamethrower();
                animator.SetBool("fire", false);
            }

            animator.SetBool("tracking",true);

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyData.hitbox.SetActive(false);
    }
}
