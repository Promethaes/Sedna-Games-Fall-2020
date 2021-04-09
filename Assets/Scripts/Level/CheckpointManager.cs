﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GamePlayerManager playerManager;
    public EnemySpawnPoint[] enemySpawners;
    public int counter = 0;
    public UserMetricsLoggerScript uml;
    public Transform checkSpawn()
    {
        return spawnPoints[counter];
    }
    public Vector3 getSpawn()
    {
        return spawnPoints[counter].position;
    }
    public void newSpawn()
    {
        spawnPoints[counter].gameObject.SetActive(false);
        counter++;
    }
    public void reset()
    {
        foreach (EnemySpawnPoint spawner in enemySpawners)
        {
            spawner.ResetSpawnPoint();
        }

        foreach (GameObject player in playerManager.players)
        {
            player.GetComponent<PlayerBackend>().hp = player.GetComponent<PlayerBackend>().maxHP;
            player.transform.position = spawnPoints[counter].position;
            player.GetComponent<PlayerController>().downed = false;
        }
    }
}
