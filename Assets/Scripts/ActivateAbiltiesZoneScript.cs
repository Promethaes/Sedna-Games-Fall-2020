using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAbiltiesZoneScript : MonoBehaviour
{
    public PlayerManager playerManager;
    public AbilityZoneManager manager;
    //zone keeps track of what player it wants. This will help with placing the activation zones.
    public int zoneType = 1;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        manager = FindObjectOfType<AbilityZoneManager>();
        manager.abilityZones.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerManager == null)
            return;

        if (other.gameObject.GetComponent<CharMenuInput>())
            foreach (var p in playerManager.players)
                if (other.gameObject == p && p.GetComponentInChildren<NPlayerInput>().playerType == zoneType)
                    p.GetComponentInChildren<NPlayerInput>().insideCastingZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (playerManager == null)
            return;
        if (other.gameObject.GetComponent<CharMenuInput>())
            foreach (var p in playerManager.players)
                if (other.gameObject == p && p.GetComponentInChildren<NPlayerInput>().playerType == zoneType)
                    p.GetComponentInChildren<NPlayerInput>().insideCastingZone = false;
    }




}
