﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{

    public SceneChanger changer;
    public int sceneIndex=0;
    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Player"){

            var netManager = FindObjectOfType<CSNetworkManager>();
            if(netManager)
                

            changer.changeScene(sceneIndex);
        }
    }
}
