using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpageSplitter : MonoBehaviour
{
    public int poolSize = 2;
    public GameObject splitling;
    List<GameObject> _splitlings = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < poolSize;i++){
            var temp = GameObject.Instantiate(splitling);
            temp.SetActive(false);
            temp.transform.position = gameObject.transform.position;
            temp.transform.localScale = gameObject.transform.localScale * 0.5f;
            temp.GetComponent<EnemyData>().randomScale = false;
            temp.GetComponent<EnemyData>().enemyScale = gameObject.GetComponent<EnemyData>().enemyScale * 0.5f;
            temp.GetComponent<EnemyData>().Init();
            _splitlings.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable() {
        for(int i = 0; i < _splitlings.Count;i++){
            _splitlings[i].transform.position = gameObject.transform.position + new Vector3(i,1.0f,i);
            _splitlings[i].SetActive(true);
        }
    }
}
