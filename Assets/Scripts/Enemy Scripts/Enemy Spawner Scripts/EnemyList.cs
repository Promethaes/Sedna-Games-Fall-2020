using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public int spawnKey;
    public List<GameObject> enemies;

    public GameObject GetEnemyFromList()
    {
        var temp = enemies[spawnKey];
        if (temp.activeSelf)
            return null;
        spawnKey = (spawnKey + 1) % enemies.Count;
        return temp;
    }
}
