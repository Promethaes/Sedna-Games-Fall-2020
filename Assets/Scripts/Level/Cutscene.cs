﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public GameObject[] blocks;
    public Camera cam;
    float _fadeTime=2.5f;
    Color _color;
    public bool cutsceneComplete = false;
    public GameObject abilityZone;
    public GameObject effect;

    public void startCutscene()
    {
        Debug.Log("Starting Cutscene");
        if (Camera.allCameras.Length > 0)
            Camera.allCameras[0].gameObject.SetActive(false);
        cam.gameObject.SetActive(true);
        switch (abilityZone.GetComponent<ActivateAbiltiesZoneScript>().zoneType)
        {
            case PlayerType.TURTLE:
                StartCoroutine(Fade());
                break;
            case PlayerType.POLAR_BEAR:
                StartCoroutine(IceBreak());
                break;
                //TODO: create other bison/snake for w/e they do
            default:
                break;
        }
    }

    IEnumerator Fade()
    {
        //TODO: Figure out a way to get alpha of shaders instead
        _color = blocks[0].GetComponent<MeshRenderer>().material.color;
        while (_fadeTime >= 0.0f)
        {
            _fadeTime-=0.1f*Time.deltaTime;
            _color.a -=0.1f/_fadeTime*Time.deltaTime;
            for (int i=0;i<blocks.Length;i++)
                blocks[i].GetComponent<MeshRenderer>().material.SetColor("Base_Color", _color);
            yield return null;
        }
        _color.a = 1.0f;
        for (int b=0;b<blocks.Length;b++)
        {
            blocks[b].SetActive(false);
            blocks[b].GetComponent<MeshRenderer>().material.SetColor("Base_Color", _color);
        }

        yield return new WaitForSeconds(1.5f);
        Debug.Log("Ending Cutscene");
        cam.gameObject.SetActive(false);
        var players = GameObject.FindGameObjectsWithTag("Player");
        for (int i=0;i<players.Length;i++)
        {
            players[i].GetComponentInChildren<Camera>(true).gameObject.SetActive(true);
            players[i].GetComponentInChildren<PlayerController>().inCutscene = false;
        }
        cutsceneComplete = true;
        abilityZone.SetActive(false);
        effect.SetActive(false);
        //Destroy(this);
    }

    IEnumerator IceBreak()
    {
        abilityZone.SetActive(false);
        //NOTE: No effect yet
        //effect.SetActive(false);
        foreach(var x in blocks)
            x.SetActive(true);
        cutsceneComplete = true;
        yield return new WaitForSeconds(1.5f);
        cam.gameObject.SetActive(false);
        var players = GameObject.FindGameObjectsWithTag("Player");
        for (int i=0;i<players.Length;i++)
        {
            players[i].GetComponentInChildren<Camera>(true).gameObject.SetActive(true);
            players[i].GetComponentInChildren<PlayerController>().inCutscene = false;
        }
    }
}

