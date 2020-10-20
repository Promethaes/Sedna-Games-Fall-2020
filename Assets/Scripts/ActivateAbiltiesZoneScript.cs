using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAbiltiesZoneScript : MonoBehaviour
{
    public PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerManager == null)
            return;

        if (other.gameObject.GetComponent<CharMenuInput>())
            foreach (var p in playerManager.players)
                if (other.gameObject == p)
                    p.GetComponentInChildren<NPlayerInput>().insideCastingZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (playerManager == null)
            return;
        if (other.gameObject.GetComponent<CharMenuInput>())
            foreach (var p in playerManager.players)
                if (other.gameObject == p)
                    p.GetComponentInChildren<NPlayerInput>().insideCastingZone = false;
    }




}
