using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpageSplitter : MonoBehaviour
{
    public List<GameObject> splitlings = new List<GameObject>();
    List<GameObject> _splitlings = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < splitlings.Count; i++)
        {
            var temp = GameObject.Instantiate(splitlings[i]);
            temp.SetActive(false);
            temp.transform.position = gameObject.transform.position;
            temp.transform.localScale = gameObject.transform.localScale * 0.75f;
            temp.GetComponent<EnemyData>().randomScale = false;
            temp.GetComponent<EnemyData>().enemyScale = gameObject.GetComponent<EnemyData>().enemyScale * 0.75f;
            temp.GetComponent<EnemyData>().Init();
            _splitlings.Add(temp);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _splitlings.Count; i++)
        {
            if (!_splitlings[i])
                continue;
            _splitlings[i].transform.position = gameObject.transform.position;
            _splitlings[i].SetActive(true);
        }
    }
}
