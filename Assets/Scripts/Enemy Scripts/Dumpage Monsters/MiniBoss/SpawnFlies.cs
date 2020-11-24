using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlies : MonoBehaviour
{
    public EnemyData enemyData;

    public GameObject prefab;

    List<GameObject> flies = new List<GameObject>();

    public int poolSize = 4;

    float _lastHP = 0.0f;

    int _key = 0;

    GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var temp = GameObject.Instantiate(prefab);
            temp.SetActive(false);
            temp.transform.position = gameObject.transform.position;
            flies.Add(temp);
        }

        players = GameObject.FindGameObjectsWithTag("Player");
        _lastHP = enemyData._health;
    }
    public void Spawn()
    {
        if (flies[_key].activeSelf)
        {
            _key = (_key + 1) % flies.Count;
            return;
        }
        flies[_key].SetActive(true);
        flies[_key].transform.position = gameObject.transform.position + gameObject.transform.forward;


        float shortestMag = 1000.0f;
        GameObject closestPlayer = null;
        foreach (GameObject player in players)
        {
            float lMag = (gameObject.transform.position - player.transform.position).magnitude;
            if (lMag < shortestMag)
            {
                shortestMag = lMag;
                closestPlayer = player;
            }
        }

        flies[_key].GetComponent<Flies>().target = closestPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyData._health - _lastHP != 0.0f)
        {
            Spawn();
        }

        _lastHP = enemyData._health;
    }
}
