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
    bool _shouldSpawn = false;
    public float spawnTimeInterval = 1.0f;
    float _pvtSpawnTimeInterval = 1.0f;
    void Start()
    {
        CreatePool();
    }

    void Update()
    {
        _pvtSpawnTimeInterval -= Time.deltaTime;
        if (_pvtSpawnTimeInterval <= 0.0f && _shouldSpawn)
            SpawnEnemy();
    }
    void CreatePool()
    {
        for (int i = 0; i < maxSpawn; i++)
        {
            spawnEnemies.Add(GameObject.Instantiate(enemyPrefab));
            spawnEnemies[i].SetActive(false);
        }
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
        _pvtSpawnTimeInterval = spawnTimeInterval;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharMenuInput>())
            _shouldSpawn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<CharMenuInput>())
            _shouldSpawn = false;
    }

}
