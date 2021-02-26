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
            allDone = !esp.AnyEnemiesActive();

        if (allDone)
            gameObject.SetActive(false);
    }
}
