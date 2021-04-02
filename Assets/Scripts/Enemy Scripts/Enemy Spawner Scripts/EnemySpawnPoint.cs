﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public static List<EnemySpawnPoint> AllEnemySpawnPoints = new List<EnemySpawnPoint>();
    public int spawnPointIndex = -1;

    //the particular enemy you want to spawn here
    public GameObject enemyPrefab;
    public List<GameObject> spawnEnemies = new List<GameObject>();
    public float spawnRadiusScalar = 15.0f;
    public int maxSpawn = 5;
    public bool oneTimeSpawn = false;
    public bool randomizeSpawnPos = false;
    bool _shouldSpawn = false;
    public float spawnTimeInterval = 1.0f;
    float _pvtSpawnTimeInterval = 1.0f;
    public Barriers _barriers;
    public bool overrideAndClear = false;

    public CSNetworkManager networkManager = null;

    public bool sendDesyncUpdate = false;

    void Start()
    {
        AllEnemySpawnPoints.Add(this);
        spawnPointIndex = AllEnemySpawnPoints.Count - 1;

        networkManager = FindObjectOfType<CSNetworkManager>();

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

    void HandleDoneSpawning()
    {
        if (AnyEnemiesActive())
            return;

        _barriers.barrierDown();
        var temp = spawnEnemies[0].GetComponentInChildren<EnemyData>().getPlayers();
        for (int i = 0; i < temp.Length; i++)
            temp[i].GetComponentInChildren<PlayerController>().outOfCombat = true;
    }

    float desyncTimer = 0.0f;
    void EnemyDesyncUpdate()
    {
        if (!networkManager.isHostClient)
            return;

        desyncTimer -= Time.deltaTime;
        if (desyncTimer <= 0.0f)
        {
            for (int i = 0; i < spawnEnemies.Count; i++)
                if (spawnEnemies[i].activeSelf)
                    networkManager.SendEnemyDesyncUpdate(spawnPointIndex, i, spawnEnemies[i].transform.position);

            desyncTimer = 10.0f;
        }

    }

    void Update()
    {
        if (sendDesyncUpdate)
            EnemyDesyncUpdate();

        if (overrideAndClear)
            KillSpawnPoint();
        if (oneTimeSpawn)
        {
            HandleDoneSpawning();
            return;
        }
        _pvtSpawnTimeInterval -= Time.deltaTime;
        if (_pvtSpawnTimeInterval <= 0.0f && _shouldSpawn)
            SpawnEnemy();
        if (!_shouldSpawn)
            HandleDoneSpawning();
    }

    void KillSpawnPoint()
    {
        foreach (var e in spawnEnemies)
            e.SetActive(false);

        HandleDoneSpawning();
    }

    void CreatePool()
    {
        for (int i = 0; i < maxSpawn; i++)
        {
            spawnEnemies.Add(GameObject.Instantiate(enemyPrefab));
            spawnEnemies[i].SetActive(false);
            spawnEnemies[i].GetComponent<EnemyData>().spawnPointIndex = spawnPointIndex;
            spawnEnemies[i].GetComponent<EnemyData>().enemyIndex = i;
        }
    }

    public bool AnyEnemiesActive()
    {
        for (int i = 0; i < spawnEnemies.Count; i++)
        {
            if (spawnEnemies[i].activeSelf)
                return true;
        }
        return false;
    }
    void SpawnEnemy()
    {
        int spawnIndex = -1;

        if (_pvtSpawnTimeInterval > 0.0f)
            return;

        for (int i = 0; i < spawnEnemies.Count; i++)
        {
            if (!spawnEnemies[i].activeSelf)
                spawnIndex = i;
        }
        if (spawnIndex == -1)
            return;//no availible enemy spawns


        float radius = gameObject.GetComponent<SphereCollider>().radius;

        Vector3 placeVec = new Vector3(radius + spawnIndex, 0.0f, radius + spawnIndex);
        if (randomizeSpawnPos)
            placeVec = new Vector3(Random.Range(-radius, radius), 0.0f, Random.Range(-radius, radius));

        spawnEnemies[spawnIndex].transform.position = gameObject.transform.position + placeVec * spawnRadiusScalar;
        spawnEnemies[spawnIndex].SetActive(true);
        var enemy = spawnEnemies[spawnIndex].GetComponentInChildren<EnemyData>();
        enemy.health = enemy.maxHealth;
        enemy.healthBar.sizeDelta = new Vector2(enemy.getHealth() / enemy.getMaxHealth() * 90.0f, enemy.healthBar.sizeDelta.y);
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
            _barriers.barrierUp();
            return;
        }
        if (other.gameObject.tag == "Enemy")
        {
            foreach (var e in spawnEnemies)
                if (e == other.gameObject)
                    return;
            spawnEnemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            _shouldSpawn = false;
    }

    private void OnDisable()
    {
        var backends = FindObjectsOfType<PlayerBackend>();
        if (backends.Length != 0)
        {
            foreach (var back in backends)
            {
                back.playerController.downed = false;
                back.hp = back.maxHP;
            }
        }
    }
}
