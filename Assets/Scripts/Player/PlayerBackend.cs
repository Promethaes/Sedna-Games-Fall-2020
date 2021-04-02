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
    public float invinceDuration = 0.25f;
    public CombatFeedbackDisplay feedbackDisplay;
    CSNetworkManager networkManager;
    private void Start()
    {
        manager = FindObjectOfType<CheckpointManager>();
        networkManager = FindObjectOfType<CSNetworkManager>();
        feedbackDisplay.feedback.flickerDuration = invinceDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager)
        {
            Debug.Log(gameObject.name + " Attempting to get checkpoint manager, since it was null on update");
            manager = FindObjectOfType<CheckpointManager>();
            if (manager)
                Debug.Log(gameObject.name + " Found the checkpoint manager");
        }
        if (hp <= 0.0f)
            KillPlayer();
        if (hp > maxHP)
            hp = maxHP;
    }

    IEnumerator InvinceFrame()
    {
        yield return new WaitForSeconds(invinceDuration);
        invuln = false;
    }

    public void takeDamage(float dmg, float knockbackScalar = 60.0f, bool local = true)
    {
        if (!invuln)
        {
            hp -= dmg - (dmg * turtleBuff.GetHashCode() * 0.1f);
            GetComponent<Rigidbody>().AddForce(-(GetComponent<PlayerController>()._playerMesh.transform.forward) * knockbackScalar * (dmg / 10.0f), ForceMode.Impulse);
            invuln = true;
            StartCoroutine("InvinceFrame");
            feedbackDisplay.OnTakeDamage();

            if (local)
                networkManager.SendCurrentHP(hp);
        }
    }

    public void KillPlayer()
    {
        if (!manager)
        {
            Debug.LogError("CheckpointManager was null! Could not kill Players!");
            return;
        }
        else if (!manager.playerManager)
        {
            Debug.LogError("CheckpointManager.PlayerManager was null! Could not kill Players!");
            return;
        }
        else if (manager.playerManager.players == null)
        {
            Debug.LogError("CheckpointManager.PlayerManager.Players was null! Could not kill Players!");
            return;
        }

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
