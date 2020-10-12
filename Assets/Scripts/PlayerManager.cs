using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public List<GameObject> players;
    public List<Camera> playerCameras;
    public ViewportManager viewportManager;
    public List<GameObject> playerPrefabs;

    public bool oneTimeSetup = false;

    private void Start()
    {
        var numPlayers = PlayerPrefs.GetInt("numPlayers");
        for (int i = 0; i < numPlayers; i++)
            gameObject.GetComponent<PlayerInputManager>().JoinPlayer();

    }

    void _SetAllPlayers()
    {
        var numPlayers = PlayerPrefs.GetInt("numPlayers");
        for (int i = 0; i < numPlayers; i++)
        {
            int type = PlayerPrefs.GetInt("p" + (i + 1).ToString() + "Type");
            type--;
            players[i].GetComponent<MeshFilter>().mesh = playerPrefabs[type].GetComponent<MeshFilter>().mesh;
            players[i].GetComponent<MeshRenderer>().material = playerPrefabs[type].GetComponent<MeshRenderer>().material;

        }
    }

    void setupVieports()
    {
        viewportManager.SpaceViewportsAppropriately(playerCameras.Count, playerCameras);
    }


    int _lastNumPlayers = 0;
    private void Update()
    {
        if (players.Count != _lastNumPlayers)
        {
            setupVieports();
        }
        _lastNumPlayers = playerCameras.Count;

        if (players.Count != 0 && !oneTimeSetup)
        {
            _SetAllPlayers();
            setupVieports();
            oneTimeSetup = true;
        }
    }

}
