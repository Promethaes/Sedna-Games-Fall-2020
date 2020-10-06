using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<Camera> playerCameras;
    public ViewportManager viewportManager;

    private void Start()
    {
       
    }

    void setupVieports()
    {
        viewportManager.SpaceViewportsAppropriately(playerCameras.Count, playerCameras);
    }


    int _lastNumPlayers = 0;
    private void Update()
    {
        if (playerCameras.Count != _lastNumPlayers)
            setupVieports();
        _lastNumPlayers = playerCameras.Count;
    }

}
