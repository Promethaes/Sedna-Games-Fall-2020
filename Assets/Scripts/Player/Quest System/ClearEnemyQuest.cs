using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearEnemyQuest : Quest
{
    public List<EnemySpawnPoint> spawnPoints;
    List<bool> tempBools = new List<bool>();

    private void Start()
    {
        foreach (var spawnPoint in spawnPoints)
            tempBools.Add(false);
    }
    protected override void QuestUpdate()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (!spawnPoints[i].AnyEnemiesActive())
                tempBools[i] = true;
        }
        bool tempComplete = true;
        foreach (var temp in tempBools)
        {
            if (!temp)
                tempComplete = false;
        }
        if (!completed)
            completed = tempComplete;
    }
}
