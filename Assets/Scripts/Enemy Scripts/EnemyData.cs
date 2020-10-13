using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyData : MonoBehaviour
{
    // Start is called before the first frame update
    public float searchRadius = 0;
    public int currentPatrolGoal = 0;
    public bool patrol;
    public List<Transform> destinations;
    public GameObject player;
    public NavMeshAgent agent;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
