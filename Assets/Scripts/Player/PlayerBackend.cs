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
    public PlayerController playerController;
    static List<PlayerBackend> backends = new List<PlayerBackend>();
    SoundController soundController;
    private void Start()
    {
        backends.Add(this);

        manager = FindObjectOfType<CheckpointManager>();
        networkManager = FindObjectOfType<CSNetworkManager>();
        feedbackDisplay.feedback.flickerDuration = invinceDuration;

        soundController = GetComponent<SoundController>();
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

        if (hp > maxHP)
            hp = maxHP;

        CheckReset();
    }

    static void CheckReset()
    {
        foreach (var backend in backends)
            if (backend == null)
            {
                backends.Remove(backend);
                return;
            }

        bool allDead = true;
        foreach (var back in backends)
            if (back.hp > 0.0f)
            {
                allDead = false;
                break;
            }

        if (!backends[0].manager)
        {
            Debug.LogError(backends[0].name + "Manager is null reference!");
            backends[0].manager = FindObjectOfType<CheckpointManager>();
            if (!backends[0].manager)
                Debug.LogError(backends[0].name + "Manager is null reference and it could not be found!");
        }
        else if (allDead && backends[0].manager)
            backends[0].manager.reset();

    }

    IEnumerator InvinceFrame()
    {
        yield return new WaitForSeconds(invinceDuration);
        invuln = false;
    }

    public void takeDamage(float dmg, float knockbackScalar = 60.0f)
    {
        if (!invuln)
        {
            hp -= dmg - (dmg * turtleBuff.GetHashCode() * 0.1f);
            GetComponent<Rigidbody>().AddForce(-(GetComponent<PlayerController>()._playerMesh.transform.forward) * knockbackScalar * (dmg / 10.0f), ForceMode.Impulse);
            invuln = true;
            StartCoroutine("InvinceFrame");
            feedbackDisplay.OnTakeDamage();

            soundController.PlayPainSound();

            if (!gameObject.name.Contains("REMOTE"))
                networkManager.SendCurrentHP(hp);
            if (hp <= 0.0f)
            {
                playerController.downed = true;
                soundController.PlayKOSound();
            }

        }
    }
}
