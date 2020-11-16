using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackend : MonoBehaviour
{
    public float hp = 100.0f;
    public float maxHP = 100.0f;
    public CheckpointManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<CheckpointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0.0f)
            KillPlayer();
    }

    public void KillPlayer()
    {

        int _counter = 0;
        foreach (GameObject player in GameObject.Find("PlayerManager").GetComponent<PlayerManager>().players)
        {
            if (player.GetComponent<PlayerBackend>().hp <= 0.0f)
            {
                _counter++;
                player.GetComponentInChildren<NPlayerInput>()._downed = true;
            }
        }
        if (_counter == GameObject.Find("PlayerManager").GetComponent<PlayerManager>().players.Count)
        {
            manager.reset();
        }
    }
}
