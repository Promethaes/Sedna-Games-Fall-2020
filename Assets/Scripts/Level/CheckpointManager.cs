using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GamePlayerManager playerManager;
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
        var enemySpawners = FindObjectsOfType<EnemySpawnPoint>();
        foreach (EnemySpawnPoint spawner in enemySpawners)
        {
            spawner.ResetSpawnPoint();
        }

        var players = FindObjectsOfType<PlayerController>();
        foreach (var player in players)
        {
            player.gameObject.GetComponent<PlayerBackend>().hp = player.gameObject.GetComponent<PlayerBackend>().maxHP;
            player.gameObject.transform.position = spawnPoints[counter].position;
            player.gameObject.GetComponent<PlayerController>().downed = false;
        }
    }
}
