using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrbManager : MonoBehaviour
{
    public static HealthOrbManager healthOrbManager;
    public static HealthOrbManager GetHealthOrbManager()
    {
        return healthOrbManager;
    }
    public GameObject healthOrbs;
    int maxOrbs = 0;
    Queue<GameObject> _orbPool = new Queue<GameObject>();
    void Awake()
    {
        if (healthOrbManager == null)
            healthOrbManager = this;
        else if (healthOrbManager != this)
            Destroy(gameObject);
    }

    public void getOrb(Vector3 pos)
    {
        if (_orbPool.Count <= 0)
        {
            GameObject temp = Instantiate(healthOrbs, pos, Quaternion.identity);
            temp.transform.parent = this.transform;
            _orbPool.Enqueue(temp);
            maxOrbs++;
        }
        GameObject orb = _orbPool.Dequeue();
        orb.SetActive(true);
        orb.transform.position = pos;
    }

    public void setOrb(GameObject orb)
    {
        _orbPool.Enqueue(orb);
    }
}
