using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public List<GameObject> players;
    public ViewportManager viewportManager;
    public List<GameObject> playerPrefabs;


    private void Start()
    {
        var numPlayers = PlayerPrefs.GetInt("numPlayers");

    }

    void _SetAllPlayers()
    {
        var numPlayers = PlayerPrefs.GetInt("numPlayers");
        for (int i = 0; i < players.Count; i++)
        {
            int type = PlayerPrefs.GetInt("p" + (i + 1).ToString() + "Type");
            type--;
            players[i].GetComponent<MeshFilter>().mesh = playerPrefabs[type].GetComponent<MeshFilter>().sharedMesh;
            players[i].GetComponent<MeshRenderer>().material = playerPrefabs[type].GetComponent<MeshRenderer>().sharedMaterial;

        }
    }

    void setupVieports()
    {
        var temp = new List<Camera>();

        foreach (var p in players)
            temp.Add(p.GetComponentInChildren<Camera>());

        viewportManager.SpaceViewportsAppropriately(players.Count, temp);
    }


    int _lastNumPlayers = 0;
    private void Update()
    {
        if (players.Count != _lastNumPlayers)
        {
            setupVieports();
        }
        _lastNumPlayers = players.Count;

    }

}
