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




    public float _health;
    public float[] _range = new float[3];
    public float _animationDuration = 0.5f;
    public int _comboCounter = 0;
    public float[] _damageValues = new float[3];
    public float[] _animationDelay = new float[3];

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
                setCombo(15.0f, 15.0f, 25.0f, 0.6f, 0.6f, 1.8f, 2.5f, 2.5f, 3.5f);
                break;
            case enemyType.chaotic_Water_Spirit:
                setHealth(80.0f);
                setCombo(15.0f, 15.0f, 25.0f, 1.5f, 1.5f, 3.0f, 10.0f, 10.0f, 10.0f);
                break;
            case enemyType.dumpage_Monster:
                setHealth(250.0f);
                setCombo(20.0f, 20.0f, 35.0f, 1.5f, 1.5f, 2.7f, 0.8f, 1.1f, 1.3f);
                break;
            case enemyType.icePick:
                setHealth(120.0f);
                setCombo(8.0f, 12.0f, 20.0f, 0.8f, 1.2f, 1.7f, 10.9f, 11.0f,11.1f);
                icePickEnemies.Add(this.gameObject);
                break;
            default:
                break;
        }

        player = GameObject.FindGameObjectWithTag("Player");
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


    protected void setCombo(float x, float y, float z, float u, float v, float w, float r, float s, float t)
    {
        //Damage values for combo hits 1/2/3, animation length for combo hits 1/2/3
        _damageValues[0] = x;
        _damageValues[1] = y;
        _damageValues[2] = z;
        _animationDelay[0] = u;
        _animationDelay[1] = v;
        _animationDelay[2] = w;
        _range[0] = r;
        _range[1] = s;
        _range[2] = t;
    }
}
