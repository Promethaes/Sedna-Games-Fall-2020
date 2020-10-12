using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToPCameraList : MonoBehaviour
{
    public PlayerManager pManager;
    // Start is called before the first frame update
    void Start()
    {
        pManager.playerCameras.Add(gameObject.GetComponent<Camera>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
