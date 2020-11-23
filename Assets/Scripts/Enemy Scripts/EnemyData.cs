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

    [SerializeField]
    private GameObject[] players;
    private NavMeshAgent agent;



    public float _maxHealth;
    public float _health;
    public float _range;
    public float _damageValues;

    public enum enemyType
    {
        dumpage_Monster,
        icePick,
        buckthorn,
        chaotic_Water_Spirit,
        flinger,
        dumpageMiniBoss,
    }

    public enemyType enemy = enemyType.buckthorn;

    public float icePickRange = 5.0f;
    public static List<GameObject> icePickEnemies = new List<GameObject>();

    public bool randomScale = true;
    public float enemyScale = 1.0f;
    public float lowerBoundEnemyScale = 0.5f;
    public float upperBoundEnemyScale = 1.5f;

    void DetermineEnemyScale()
    {
        enemyScale = Random.Range(lowerBoundEnemyScale, upperBoundEnemyScale);
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * enemyScale;
    }

    public void Init()
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
            case enemyType.flinger:
                setHealth(150.0f);
                setCombo(15.0f, 30.0f);
                break;
            case enemyType.dumpageMiniBoss:
                setHealth(350.0f);
                setCombo(20.0f, 30.0f);
                break;
            default:
                break;
        }

        searchRadius *= enemyScale;

        players = GameObject.FindGameObjectsWithTag("Player");
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if (randomScale)
            DetermineEnemyScale();
        Init();
    }

    public void setHealth(float hp)
    {
        _maxHealth = hp * enemyScale;
        _health = hp * enemyScale;
    }

    public float getHealth()
    {
        return _health;
    }
    public float getMaxHealth()
    {
        return _maxHealth;
    }
    public void takeDamage(float hp)
    {
        _health -= hp;
        if (_health <= 0.0f)
            die();
    }

    protected void die()
    {
        if (_health <= 0.0f)
            //TODO: Dying animation, loot drops, etc.
            gameObject.SetActive(false);
    }


    protected void setCombo(float damage, float range)
    {
        //Damage values for combo hits 1/2/3, animation length for combo hits 1/2/3
        _damageValues = damage * enemyScale;
        _range = range * enemyScale;
    }

    public NavMeshAgent getNavMeshAgent()
    {
        return agent;
    }

    public GameObject[] getPlayers()
    {
        return players;
    }
}
