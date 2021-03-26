using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackend : MonoBehaviour
{
    public float hp = 100.0f;
    public float maxHP = 100.0f;
    public CheckpointManager manager;
    public bool turtleBuff = false;
    public bool invuln = false;
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

    IEnumerator InvinceFrame()
    {
        yield return new WaitForSeconds(1.0f);
        invuln = false;
    }

    public void takeDamage(float dmg,float knockbackScalar = 60.0f)
    {
        if (!invuln)
        {
            hp -= dmg - (dmg * turtleBuff.GetHashCode() * 0.1f);
            GetComponent<Rigidbody>().AddForce(-(GetComponent<PlayerController>()._playerMesh.transform.forward)*knockbackScalar*(dmg / 10.0f),ForceMode.Impulse);
            invuln = true;
            StartCoroutine("InvinceFrame");
        }
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
