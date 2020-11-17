using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackend : MonoBehaviour
{
    public float hp = 100.0f;
    public float maxHP = 100.0f;
    public CheckpointManager manager;

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0.0f)
            KillPlayer();
    }

    public void KillPlayer()
    {
        manager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
        int _counter = 0;
        for (int i = 0;i<manager.playerManager.players.Count;i++)
        {
            GameObject player = manager.playerManager.players[i];
            if (player.GetComponent<PlayerBackend>().hp <= 0.0f)
            {
                _counter++;
                player.GetComponentInChildren<NPlayerInput>()._downed = true;
            }
        }
        if (_counter == manager.playerManager.players.Count)
        {
            manager.reset();
        }
    }
}
