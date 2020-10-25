using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public float spawnRadiusScalar = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().spawnPoints.Add(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnEnemy(GameObject enemy)
    {
        float radius = gameObject.GetComponent<SphereCollider>().radius;

        enemy.transform.position = gameObject.transform.position + new Vector3(Random.Range(-radius,radius),0.0f,Random.Range(-radius,radius))*spawnRadiusScalar;
        enemy.SetActive(true);
    }

}
