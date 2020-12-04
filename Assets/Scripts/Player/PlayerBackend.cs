using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackend : MonoBehaviour
{
    public float hp = 100.0f;
    public float maxHP = 100.0f;
    public CheckpointManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0.0f)
            KillPlayer();
        if (hp > maxHP)
            hp = maxHP;
    }

    public void KillPlayer()
    {


        int _counter = 0;
        for (int i = 0; i < manager.playerManager.players.Count; i++)
        {
            GameObject player = manager.playerManager.players[i];
            if (player.GetComponent<PlayerBackend>().hp <= 0.0f)
            {
                _counter++;
                player.GetComponent<PlayerController>().downed = true;
            }
            if (player == gameObject)
                manager.uml.csLogDeath(new UserMetricsLoggerScript.Death("temp", Time.time, i + 1));

        }
        if (_counter == manager.playerManager.players.Count)
        {
            manager.reset();
        }
    }
}
