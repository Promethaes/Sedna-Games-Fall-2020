using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/*
 * https://answers.unity.com/questions/324589/how-can-i-tell-when-a-navmesh-has-reached-its-dest.html
 * used this thinking it would solve the problem of the ai skipping patrol points, it did not but its still an upgrade of what i had here previously
 * 
 */

public class EnemyPatrol : MonoBehaviour
{
    public NavMeshAgent agent;
    public int currentPatrolGoal = 0;
    public List<Transform> destinations;
    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(destinations[currentPatrolGoal].position);

    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    if (currentPatrolGoal < destinations.Count-1)
                    {
                        currentPatrolGoal ++;
                    }
                    else
                    {
                        currentPatrolGoal = 0;
                    }
                    agent.SetDestination(destinations[currentPatrolGoal].position);
                }
            }
        }
        
    }
}
