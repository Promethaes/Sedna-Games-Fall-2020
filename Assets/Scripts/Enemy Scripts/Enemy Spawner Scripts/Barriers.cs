using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barriers : MonoBehaviour
{
    public List<EnemySpawnPoint> spawners;
    public List<GameObject> _barriers;

    public void barrierUp()
    {
        if (_barriers.Count == 0)
            return;
        if (!_barriers[0].activeSelf)
            for (int i = 0; i < _barriers.Count; i++)
                _barriers[i].SetActive(true);
    }

    public void barrierDown()
    {
        if (_barriers.Count == 0)
            return;

        for (int i = 0; i < spawners.Count; i++)
            if (spawners[i].AnyEnemiesActive())
                return;

        for (int i = 0; i < _barriers.Count; i++)
            _barriers[i].SetActive(false);
    }
}
