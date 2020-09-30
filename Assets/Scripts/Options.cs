using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{

    //@Temporary: only for debugging. Will delete later
    public bool lockCurser = true;

    public PlayerManager playerManager;
    public ViewportManager viewportManager;
    public int numPlayers = 1;

    bool enableCouchCoop = true;

    // Start is called before the first frame update
    void Start()
    {
        setupPlayersAndViewports();
    }

    void setupPlayersAndViewports()
    {
        playerManager.ActivePlayerSetup(numPlayers);

        List<Camera> tempCameras = new List<Camera>();

        for (int i = 0; i < 4; i++)
            tempCameras.Add(playerManager.players[i].GetComponentInChildren<Camera>());

        viewportManager.SpaceViewportsAppropriately(numPlayers, tempCameras);
    }

    int _lastNumPlayers = 0;
    // Update is called once per frame
    void Update()
    {
        if (lockCurser)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;

        //optimization
        if (numPlayers != _lastNumPlayers)
            setupPlayersAndViewports();
        _lastNumPlayers = numPlayers;
    }
}
