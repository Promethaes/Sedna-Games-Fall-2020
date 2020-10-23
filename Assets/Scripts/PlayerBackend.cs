using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackend : MonoBehaviour
{
    public float hp = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0.0f)
            KillPlayer();
    }

    public void KillPlayer()
    {
        //not sure howe we're gonna implement this yet
        GameObject.Find("PlayerManager").GetComponent<PlayerManager>().players = new List<GameObject>();
        Destroy(gameObject);
    }
}
