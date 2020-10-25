using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    int _maxEnemy = 5;

    public List<EnemyList> enemyList = new List<EnemyList>();

    public List<EnemySpawnPoint> spawnPoints = new List<EnemySpawnPoint>();

    //@temp delete me later
    float _spawnTimeInterval = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var prefab in enemyPrefabs)
        {
            var tempList = new List<GameObject>();

            for (int i = 0; i < _maxEnemy; i++)
            {
                tempList.Add(GameObject.Instantiate(prefab));
                tempList[i].gameObject.SetActive(false);
            }

            EnemyList tempStruct = new EnemyList();
            tempStruct.enemies = tempList;

            enemyList.Add(tempStruct);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimeInterval -= Time.deltaTime;
        if (_spawnTimeInterval <= 0.0f)
        {
            _spawnTimeInterval = 1.0f;
            SpawnEnemy(0, 0);
        }

    }

    public void SpawnEnemy(int enemyTypeIndex, int spawnPointIndex)
    {
        var temp = enemyList[enemyTypeIndex].GetEnemyFromList();

        if (temp != null)
            spawnPoints[spawnPointIndex].spawnEnemy(temp);
    }

}
