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

    public CombatFeedbackDisplay feedbackDisplay;


    public enum enemyType
    {
        buckthorn,
        chaotic_Water_Spirit,
        dumpage_Monster,
        splitter,
        flinger,
        dumpageMiniBoss,
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

    //Networking
    public int spawnPointIndex; // index of the spawn point that this player belongs to
    public int enemyIndex; // index of this enemy in the spawn point

    void DetermineEnemyScale()
    {
        enemyScale = Random.Range(lowerBoundEnemyScale, upperBoundEnemyScale);
        transform.localScale = transform.localScale * enemyScale;
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
            case enemyType.pickRobro:
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
                setHealth(1000.0f);
                setCombo(30.0f);
                break;
            default:
                break;
        }



        healthBarBackground.sizeDelta = new Vector2(health / maxHealth * healthBarSize, healthBar.sizeDelta.y);
        healthBar.sizeDelta = new Vector2(health / maxHealth * healthBarSize, healthBar.sizeDelta.y);
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

    private void OnDisable()
    {
        Debug.Log(gameObject.name);
        var networkManager = FindObjectOfType<CSNetworkManager>();
        if (networkManager)
            networkManager.SendEnemyDeath(spawnPointIndex, enemyIndex);
        
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

    IEnumerator ResetKinematics()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("kinematic");
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void ActivateResetKinematicsCoroutine()
    {
        StartCoroutine("ResetKinematics");
    }

    public void takeDamage(float hp, float knockbackScalar = 20.0f)
    {
        health -= hp;
        healthBar.sizeDelta = new Vector2(health / maxHealth * healthBarSize, healthBar.sizeDelta.y);
        billboard.healthChanged();
        enemySounds[(int)EnemySoundIndex.Pain].Play();
        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;

        var players = FindObjectsOfType<PlayerController>();

        float shortestMag = 1000.0f;
        Vector3 direction = Vector3.zero;
        foreach (var p in players)
        {
            var vec = gameObject.transform.position - p.transform.position;
            if (vec.magnitude < shortestMag)
            {
                shortestMag = vec.magnitude;
                direction = vec.normalized;
            }
        }

        rigidBody.AddForce(Secrets.limitKnockBack(direction * knockbackScalar * (hp / 10.0f)), ForceMode.Impulse);
        StartCoroutine("ResetKinematics");
        feedbackDisplay.OnTakeDamage();
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
        
        var leaderboard = FindObjectOfType<LeaderboardMetricsManager>();
        if(leaderboard)
            leaderboard.enemiesDefeated++;

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
        damageValues *= 0.8f;
        while (_poisonDuration > 0.0f)
        {
            takeDamage(10.0f);
            yield return new WaitForSeconds(1.0f);
            _poisonDuration -= 1.0f;
        }
        damageValues /= 0.8f;
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

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player")
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        else if (other.gameObject.tag == "Enemy" && other.gameObject.GetComponent<Rigidbody>().isKinematic)
        {
            var rigid = other.gameObject.GetComponent<Rigidbody>();
            rigid.isKinematic = false;
            rigid.AddForce(gameObject.GetComponent<Rigidbody>().velocity, ForceMode.Impulse);
            other.gameObject.GetComponent<EnemyData>().ActivateResetKinematicsCoroutine();

        }
    }
}
