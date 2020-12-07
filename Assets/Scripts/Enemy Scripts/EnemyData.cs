using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
enum EnemySoundIndex
{
    Spawn,
    Attack,
    Pain,
    Die
}
public class EnemyData : MonoBehaviour
{
    public float searchRadius = 0;
    public int currentPatrolGoal = 0;
    public bool patrol;
    public List<Transform> destinations;

    [SerializeField]
    private GameObject[] players;
    private NavMeshAgent agent;
    public List<FMODUnity.StudioEventEmitter> enemySounds = new List<FMODUnity.StudioEventEmitter>();


    public Billboard _billboard;
    public RectTransform _healthBar;
    public RectTransform _healthBarBackground;
    public float _healthBarSize = 90.0f;
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
    public float enemyScale = 1.5f;
    public float lowerBoundEnemyScale = 1.0f;
    public float upperBoundEnemyScale = 2.0f;

    void DetermineEnemyScale()
    {
        enemyScale = Random.Range(lowerBoundEnemyScale, upperBoundEnemyScale);
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * enemyScale;
        searchRadius *= enemyScale;
        foreach (var sound in enemySounds)
        {
            sound.SetParameter("size", enemyScale / upperBoundEnemyScale);
        }
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



        _healthBarBackground.sizeDelta = new Vector2(_health/_maxHealth*_healthBarSize, _healthBar.sizeDelta.y);
        _healthBar.sizeDelta = new Vector2(_health/_maxHealth*_healthBarSize, _healthBar.sizeDelta.y);
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
        _healthBar.sizeDelta = new Vector2(_health/_maxHealth*_healthBarSize, _healthBar.sizeDelta.y);
        _billboard.healthChanged();
        enemySounds[(int)EnemySoundIndex.Pain].Play();
        if (_health <= 0.0f)
            die();
    }

    protected void die()
    {
        if (_health <= 0.0f)
        {
            enemySounds[(int)EnemySoundIndex.Die].Play();
            //TODO: Dying animation, loot drops, etc.
            if (Random.Range(0.0f, 1.0f) < 0.2f)
                HealthOrbManager.GetHealthOrbManager().getOrb(transform.position);
        }
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
