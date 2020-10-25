using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarBearScript : MonoBehaviour
{
    public List<GameObject> swimPositionMarkers = new List<GameObject>();
    public GameObject player;
    public bool swimming = false;

    public float transitionCooldown = 2.0f;
    float _switchCooldown = 2.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _switchCooldown -= Time.deltaTime;
        if (_switchCooldown <= 0.0f)
            _switchCooldown = 0.0f;
    }


    public void Transition()
    {
        if (_switchCooldown != 0.0f)
            return;

        if(swimming)
        _transitionToWalkMode();
        else
        _transitionIntoSwimMode();

        _switchCooldown = transitionCooldown;
    }

    void _transitionIntoSwimMode()
    {
        GameObject closestMarker = null;
        float closestMag = 10000.0f;
        foreach (var marker in swimPositionMarkers)
        {
            var temp = (marker.transform.position - player.transform.position).magnitude;
            if (temp < closestMag)
            {
                closestMag = temp;
                closestMarker = marker;
            }
        }

        //add some sort of flare later. like lerping or some shit idk
        player.transform.position = closestMarker.transform.position;
        swimming = true;
    }

    void _transitionToWalkMode()
    {
        var manager = FindObjectOfType<AbilityZoneManager>();
        GameObject closestMarker = null;
        float closestMag = 10000.0f;
        foreach (var marker in manager.abilityZones)
        {
            var temp = (marker.transform.position - player.transform.position).magnitude;
            if (temp < closestMag)
            {
                closestMag = temp;
                closestMarker = marker.gameObject;
            }
        }
        player.transform.position = closestMarker.transform.position;
        swimming = false;
    }


}
