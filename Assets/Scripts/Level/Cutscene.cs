using System.Collections;
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
        _color = blocks[0].GetComponent<MeshRenderer>().material.color;
        if (Camera.allCameras.Length > 0)
            Camera.allCameras[0].gameObject.SetActive(false);
        cam.gameObject.SetActive(true);
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        //while (_fadeTime >= 0.0f)
        //{
        //    _fadeTime-=0.1f*Time.deltaTime;
        //    //_color.a -=0.1f/_fadeTime*Time.deltaTime;
        //    //for (int i=0;i<blocks.Length;i++)
        //    //    blocks[i].GetComponent<MeshRenderer>().material.SetColor("Base_Color", _color);
        //    yield return null;
        //}
        //_color.a = 1.0f;
        for (int b=0;b<blocks.Length;b++)
        {
            blocks[b].SetActive(false);
           // blocks[b].GetComponent<MeshRenderer>().material.SetColor("Base_Color", _color);
        }

        yield return new WaitForSecondsRealtime(1.5f);
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
}

