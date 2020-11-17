using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [Header("Lists")]
    public List<GameObject> playerPrefabs;
    public List<GameObject> players;
    [Header("Scene references")]

    [Space]
    public bool oneTimeSetup = true;

    public ViewportManager viewportManager;


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

    void setupViewports()
    {
        var temp = new List<Camera>();

        foreach (var p in players)
            temp.Add(p.GetComponentInChildren<Camera>());

        viewportManager.SpaceViewportsAppropriately(players.Count, temp);
    }


    int _lastNumPlayers = 0;
    private void Update() {
        if(players.Count == 0 && oneTimeSetup) {
            // Scene index 3 should be the game over scene. Please update this if this is not the case.
            GameObject.Find("SceneChanger").GetComponent<SceneChanger>().changeScene(3);
            Destroy(gameObject);
            return; // Don't bother doing anything else if we need to change scenes (not sure if LoadScene already does this...@Cleanup?)
        }

        if (players.Count != _lastNumPlayers) {
            setupViewports();
        }
        _lastNumPlayers = players.Count;

    }

}
