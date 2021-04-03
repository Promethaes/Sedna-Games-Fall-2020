using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class LeaderboardScript : MonoBehaviour
{
    public GameObject leaderboardStatPrefab;
    public Color buttonColourOne;
    public Color buttonColourTwo;
    public bool test = false;
    int numPrefabsIn = 0;


    //Networking
    Client _leaderboardClient = null;
    public string serverIP;
    Thread recThread = null;

    void SendLeaderboardRequest()
    {
        _leaderboardClient.Send("leaderinfo");
        foreach(Transform child in gameObject.transform)
            Destroy(child.gameObject);
    }

    private void Start()
    {
        _leaderboardClient = new Client(serverIP);
        recThread = new Thread(_leaderboardClient.Receive);
        recThread.Start();
    }

    private void OnDestroy()
    {
        _leaderboardClient.leave = true;
        recThread.Abort();

        _leaderboardClient.clientSocket.Close();
    }

    private void Update()
    {
        if (test)
        {
            test = false;
            SpawnLeaderboardStat();
        }
    }

    public void SpawnLeaderboardStat()
    {
        var ob = GameObject.Instantiate(leaderboardStatPrefab, gameObject.transform);
        Debug.Log(ob.name);
        var img = ob.GetComponentInChildren<Image>();
        if (numPrefabsIn % 2 == 0)
            img.color = buttonColourOne;
        else
            img.color = buttonColourTwo;

        numPrefabsIn++;
    }
}
