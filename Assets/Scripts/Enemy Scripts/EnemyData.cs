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

    public bool icePick = false;
    public float icePickRange = 5.0f;
    public static List<GameObject> icePickEnemies = new List<GameObject>();

    

    void Start()
    {
        if(icePick)
            icePickEnemies.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
