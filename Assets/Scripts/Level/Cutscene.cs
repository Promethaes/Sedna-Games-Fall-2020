using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public GameObject[] blocks;
    public Camera cam;
    float _fadeTime=0.5f;
    Color _color;

    public void startCutscene()
    {
        _color = blocks[0].GetComponent<MeshRenderer>().material.color;
        if (Camera.allCameras.Length > 0)
            Camera.allCameras[0].gameObject.SetActive(false);
        cam.gameObject.SetActive(true);
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
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

        yield return new WaitForSecondsRealtime(2.0f);
        cam.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>(true).gameObject.SetActive(true);
        //Destroy(this);
    }
}

