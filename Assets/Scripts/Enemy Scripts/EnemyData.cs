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
    public GameObject[] players;
    public NavMeshAgent agent;




    public float _health;
    public float _range;
    public float _damageValues;

    public enum enemyType
    {
        dumpage_Monster,
        icePick,
        buckthorn,
        chaotic_Water_Spirit
    }

    public enemyType enemy = enemyType.buckthorn;

    public float icePickRange = 5.0f;
    public static List<GameObject> icePickEnemies = new List<GameObject>();

    

    void Start()
    {
        switch (enemy)
        {
            case enemyType.buckthorn:
                setHealth(150.0f);
                setCombo(15.0f, 2.5f);
                break;
            case enemyType.chaotic_Water_Spirit:
                setHealth(80.0f);
                setCombo(15.0f, 10.0f);
                break;
            case enemyType.dumpage_Monster:
                setHealth(250.0f);
                setCombo(20.0f, 2.0f);
                break;
            case enemyType.icePick:
                setHealth(120.0f);
                setCombo(8.0f, 10.9f);
                icePickEnemies.Add(this.gameObject);
                break;
            default:
                break;
        }

        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHealth(float hp)
    {
        _health = hp;
    }

    public float getHealth()
    {
        return _health;
    }
    public void takeDamage(float hp)
    {
        _health -= hp;
        if (_health < 0.0f)
            die();
    }

    protected void die()
    {
        if (_health < 0.0f)
            //TODO: Dying animation, loot drops, etc.
            gameObject.SetActive(false);
    }


    protected void setCombo(float damage, float range)
    {
        //Damage values for combo hits 1/2/3, animation length for combo hits 1/2/3
        _damageValues = damage;
        _range = range;
    }
}
