using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAbiltiesZoneScript : MonoBehaviour
{
    public AbilityZoneManager manager;
    //zone keeps track of what player it wants. This will help with placing the activation zones.
    public PlayerType zoneType = PlayerType.BISON;
    public Cutscene _cutscene;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<AbilityZoneManager>();
        manager.abilityZones.Add(this);
    }


    private void OnTriggerEnter(Collider other)
    {
        var pController = other.gameObject.GetComponent<PlayerController>();

        if (pController == null)
            return;

        if (other.gameObject.tag == "Player" && pController.playerType == zoneType)
        {
            pController.insideCastingZone = true;
            pController.bubbleShieldScript.cutscene = _cutscene;

            gameObject.GetComponentInChildren<TMPro.TextMeshPro>().text = "Press LB";
        }
    }

    private void OnTriggerExit(Collider other)
    {
         var pController = other.gameObject.GetComponent<PlayerController>();

        if (pController == null)
            return;

        if (other.gameObject.tag == "Player" && pController.playerType == zoneType)
        {
            pController.insideCastingZone = false;
            pController.bubbleShieldScript.cutscene = null;

            gameObject.GetComponentInChildren<TMPro.TextMeshPro>().text = "Come Closer!";
        }

    }




}
