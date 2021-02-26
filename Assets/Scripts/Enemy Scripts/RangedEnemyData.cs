using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyData : MonoBehaviour
{
    public EnemyData enemyData;
    public GameObject flingable;

    public Transform target;
    public float tossAngle = 1.0f;
    public float speed = 50.0f;

    public int poolSize = 4;

    List<GameObject> _flingables = new List<GameObject>();

    int _key = 0;


    void MakePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var temp = GameObject.Instantiate(flingable);
            temp.SetActive(false);
            temp.transform.position = gameObject.transform.position;
            temp.GetComponent<Flingable>().damage = enemyData.damageValues;
            _flingables.Add(temp);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MakePool();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool CanFling()
    {
        bool temp = false;

        foreach (var fling in _flingables)
        {
            if (!fling.activeSelf)
                temp = true;
        }

        return temp;
    }

    public void Fling()
    {
        _flingables[_key].SetActive(true);
        _flingables[_key].transform.position = gameObject.transform.position + new Vector3(0.0f, 1f, 0.0f) + gameObject.transform.forward;

        //@Temp: remove after values are set in stone
        _flingables[_key].GetComponent<Flingable>().damage = enemyData.damageValues;

        var tPos = target.position - gameObject.transform.position;

        _flingables[_key].GetComponent<Rigidbody>().velocity =
         (tPos + new Vector3(0.0f, tossAngle, 0.0f)).normalized * (speed * tPos.magnitude);
        _flingables[_key].GetComponent<Flingable>().target = target;

        _key = (_key + 1) % _flingables.Count;
    }
}
