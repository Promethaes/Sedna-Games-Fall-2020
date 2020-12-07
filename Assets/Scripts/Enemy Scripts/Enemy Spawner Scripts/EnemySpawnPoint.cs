using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{

    //the particular enemy you want to spawn here
    public GameObject enemyPrefab;
    public List<GameObject> spawnEnemies = new List<GameObject>();
    public float spawnRadiusScalar = 15.0f;
    public int maxSpawn = 5;
    public bool oneTimeSpawn = false;
    bool _shouldSpawn = false;
    bool _activeEnemies = false;
    public float spawnTimeInterval = 1.0f;
    float _pvtSpawnTimeInterval = 1.0f;
    void Start()
    {
        CreatePool();
        enemyPrefab.transform.position = gameObject.transform.position;
        if (oneTimeSpawn)
        {
            _pvtSpawnTimeInterval = 0.0f;
            spawnTimeInterval = 0.0f;
            for (int i = 0; i < spawnEnemies.Count; i++)
                SpawnEnemy();
        }
    }

    void Update()
    {
        if (oneTimeSpawn)
        {
            if (!CheckEnemy())
            {
                var temp = spawnEnemies[0].GetComponentInChildren<EnemyData>().getPlayers();
                for (int i = 0; i < temp.Length; i++)
                    temp[i].GetComponentInChildren<PlayerController>().outOfCombat = true;
            }
            return;
        }
        _pvtSpawnTimeInterval -= Time.deltaTime;
        if (_pvtSpawnTimeInterval <= 0.0f && _shouldSpawn)
            SpawnEnemy();
        if (!_shouldSpawn)
        {
            if (!CheckEnemy())
            {
                var temp = spawnEnemies[0].GetComponentInChildren<EnemyData>().getPlayers();
                for (int i = 0; i < temp.Length; i++)
                    temp[i].GetComponentInChildren<PlayerController>().outOfCombat = true;
            }
        }
    }
    void CreatePool()
    {
        for (int i = 0; i < maxSpawn; i++)
        {
            spawnEnemies.Add(GameObject.Instantiate(enemyPrefab));
            spawnEnemies[i].SetActive(false);
        }
    }

    bool CheckEnemy()
    {
        _activeEnemies = false;
        for (int i = 0; i < spawnEnemies.Count; i++)
        {
            if (spawnEnemies[i].activeSelf)
            {
                return true;
            }
        }
        return false;
    }
    void SpawnEnemy()
    {
        int spawnIndex = -1;

        if (_pvtSpawnTimeInterval > 0.0f)
            return;
        else
        {
            for (int i = 0; i < spawnEnemies.Count; i++)
            {
                if (!spawnEnemies[i].activeSelf)
                    spawnIndex = i;
            }
            if (spawnIndex == -1)
                return;//no availible enemy spawns
        }


        float radius = gameObject.GetComponent<SphereCollider>().radius;

        spawnEnemies[spawnIndex].transform.position = gameObject.transform.position + new Vector3(Random.Range(-radius, radius), 0.0f, Random.Range(-radius, radius)) * spawnRadiusScalar;
        spawnEnemies[spawnIndex].SetActive(true);
        var enemy = spawnEnemies[spawnIndex].GetComponentInChildren<EnemyData>();
        enemy._health = enemy._maxHealth;
        enemy._healthBar.sizeDelta = new Vector2(enemy.getHealth() / enemy.getMaxHealth() * 90.0f, enemy._healthBar.sizeDelta.y);
        _pvtSpawnTimeInterval = spawnTimeInterval;
    }

    public void setShouldSpawn(bool yn)
    {
        _shouldSpawn = yn;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _shouldSpawn = true;
            other.gameObject.GetComponentInChildren<PlayerController>().outOfCombat = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            _shouldSpawn = false;
    }
}
