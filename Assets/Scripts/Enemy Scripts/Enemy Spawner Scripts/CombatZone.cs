using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatZone : MonoBehaviour
{
    public List<EnemySpawnPoint> enemySpawnPoints;

    void Update()
    {
        bool allDone = true;
        foreach (var esp in enemySpawnPoints)
        {
            if (esp.AnyEnemiesActive())
            {
                allDone = false;
                break;
            }
        }

        if (allDone)
        {
            gameObject.SetActive(false);
            foreach (var esp in enemySpawnPoints)
                esp.sendDesyncUpdate = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            foreach (var esp in enemySpawnPoints)
                esp.sendDesyncUpdate = true;
    }
}
