using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
enum EnemySoundIndex
{
    Spawn,
    Attack,
    Pain,
    Die,
    Movement,
    DumpageSplit,
    DumpageThrow,
    BlasterOneShot,
    BlasterThreeShot,
    
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


    public Billboard billboard;
    public RectTransform healthBar;
    public RectTransform healthBarBackground;
    public float healthBarSize = 90.0f;
    public float maxHealth;
    public float health;
    public float damageValues;
    public GameObject hitbox;
    public bool fear = false;
    float _poisonDuration = 10.0f;


    public enum enemyType
    {
        buckthorn,
        chaotic_Water_Spirit,
        dumpage_Monster,
        splitter,
        flinger,
        dumpageMiniBoss,
        icePick,
        mechaShark,
        pickRobro,
        roboShooter,
        furnaceRobo,
    }

    public enemyType enemy = enemyType.buckthorn;
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

    //TODO: Split this by map type
    public void Init()
    {
        switch (enemy)
        {
            case enemyType.buckthorn:
                setHealth(150.0f);
                setCombo(15.0f);
                break;
            case enemyType.chaotic_Water_Spirit:
                setHealth(80.0f);
                setCombo(15.0f);
                break;
            case enemyType.dumpage_Monster:
                setHealth(250.0f);
                setCombo(20.0f);
                break;
            case enemyType.splitter:
                setHealth(120.0f);
                setCombo(10.0f);
                break;
            case enemyType.flinger:
                setHealth(160.0f);
                setCombo(20.0f);
                break;
            case enemyType.dumpageMiniBoss:
                setHealth(350.0f);
                setCombo(20.0f);
                break;
            case enemyType.icePick:
                setHealth(120.0f);
                setCombo(16.0f);
                break;
            case enemyType.roboShooter:
                setHealth(120.0f);
                setCombo(8.0f);
                break;
            case enemyType.mechaShark:
                setHealth(99999.0f);
                setCombo(15.0f);
                break;
            case enemyType.furnaceRobo:
                setHealth(360.0f);
                setCombo(30.0f);
                break;
            default:
                break;
        }



        healthBarBackground.sizeDelta = new Vector2(health/maxHealth*healthBarSize, healthBar.sizeDelta.y);
        healthBar.sizeDelta = new Vector2(health/maxHealth*healthBarSize, healthBar.sizeDelta.y);
        players = GameObject.FindGameObjectsWithTag("Player");
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if (randomScale)
            DetermineEnemyScale();
        Init();
        enemySounds[(int)EnemySoundIndex.Spawn].Play();
    }

    public void setHealth(float hp)
    {
        maxHealth = hp * enemyScale;
        health = hp * enemyScale;
    }

    public float getHealth()
    {
        return health;
    }
    public float getMaxHealth()
    {
        return maxHealth;
    }
    public bool getFear()
    {
        return fear;
    }
    public void takeDamage(float hp)
    {
        health -= hp;
        healthBar.sizeDelta = new Vector2(health/maxHealth*healthBarSize, healthBar.sizeDelta.y);
        billboard.healthChanged();
        enemySounds[(int)EnemySoundIndex.Pain].Play();
        if (health <= 0.0f)
            die();
    }

    protected void die()
    {
        if (health <= 0.0f)
        {
            enemySounds[(int)EnemySoundIndex.Die].Play();
            //TODO: Dying animation, loot drops, etc.
            if (Random.Range(0.0f, 1.0f) < 0.2f)
                HealthOrbManager.GetHealthOrbManager().getOrb(transform.position);
        }
        gameObject.SetActive(false);
    }

    public void Poison()
    {
        if (_poisonDuration > 0.0f)
            _poisonDuration = 10.0f;
        else
            StartCoroutine(PoisonDebuff());
    }

    IEnumerator PoisonDebuff()
    {
        damageValues*=0.8f;
        while(_poisonDuration > 0.0f)
        {
        takeDamage(10.0f);
        yield return new WaitForSeconds(1.0f);
        _poisonDuration-=1.0f;
        }
        damageValues/=0.8f;
        yield return null;
    }

    protected void setCombo(float damage)
    {
        //Damage values for combo hits 1/2/3, animation length for combo hits 1/2/3
        damageValues = damage * enemyScale;
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
