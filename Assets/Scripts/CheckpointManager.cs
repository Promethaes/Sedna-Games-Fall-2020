using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public PlayerManager playerManager;
    public EnemySpawnPoint[] enemySpawners;
    int counter = 0;

    private void Update() 
    {
        RaycastHit player;
        if (counter < spawnPoints.Length-1 && Physics.SphereCast(spawnPoints[counter+1].position, 10.0f, Vector3.zero, out player) && player.transform.tag == "Player")
        {
            newSpawn();
        }
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
            foreach (GameObject enemy in spawner.spawnEnemies)
            {
                enemy.SetActive(false);
                enemy.GetComponent<EnemyData>().setHealth(enemy.GetComponent<EnemyData>().getMaxHealth());
            }
            spawner.setShouldSpawn(false);
        }

        foreach (GameObject player in playerManager.players)
        {
            player.GetComponent<PlayerBackend>().hp = player.GetComponent<PlayerBackend>().maxHP;
            player.transform.position = spawnPoints[counter].position;
        }
    }
}
