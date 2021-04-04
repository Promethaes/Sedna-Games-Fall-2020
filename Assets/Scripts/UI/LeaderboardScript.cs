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
    int numPrefabsIn = 0;
    public GameObject LoadingBar;
    public TMPro.TextMeshProUGUI numWinsText;

    //Networking
    Client _leaderboardClient = null;
    public string serverIP;
    Thread recThread = null;
    void SendLeaderboardRequest()
    {
        _leaderboardClient.Send("leaderinfo");
        foreach (Transform child in gameObject.transform)
            Destroy(child.gameObject);
        StartCoroutine("ProcessLeaderboardInfo");
    }
    IEnumerator ProcessLeaderboardInfo()
    {
        LoadingBar.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        //interpret info here
        //NE name numEnemies time NE name numEnemies time...
        int numWins = 0;
        for (int i = 0; i < _leaderboardClient.backlog.Count; i++)
        {
            if (_leaderboardClient.backlog[i].Contains("?"))
            {
                var entries = _leaderboardClient.backlog[i].Split('?');

                foreach (var entry in entries)
                {
                    //i have no clue why this first entry is always empty string...
                    //maybe because it splits the string on the "left" side of the ?
                    if (entry == "")
                        continue;
                    numWins++;
                    var parts = entry.Split(' ');
                    SpawnLeaderboardStat(parts[0], int.Parse(parts[1]), parts[2]);
                }

                _leaderboardClient.backlog.RemoveAt(i);
                i--;
            }
        }

        string times = "";
        if (numWins > 1 && numWins != 0)
            times = "times!";
        else
            times = "time!";
        numWinsText.text = "Earth's Light has been saved " + numWins.ToString() + " " + times;

    }
    private void Start()
    {
        _leaderboardClient = new Client(serverIP);
        recThread = new Thread(_leaderboardClient.Receive);
        recThread.Start();
        SendLeaderboardRequest();
    }
    private void OnDestroy()
    {
        _leaderboardClient.leave = true;
        recThread.Abort();

        _leaderboardClient.clientSocket.Close();
    }
    public void SpawnLeaderboardStat(string name, int numEnemies, string time)
    {
        var ob = GameObject.Instantiate(leaderboardStatPrefab, gameObject.transform);
        var img = ob.GetComponentInChildren<Image>();
        if (numPrefabsIn % 2 == 0)
            img.color = buttonColourOne;
        else
            img.color = buttonColourTwo;

        var texts = ob.GetComponentsInChildren<TMPro.TextMeshProUGUI>();

        foreach (var text in texts)
        {
            if (text.gameObject.name == "Username")
                text.text = name;
            else if (text.gameObject.name == "E")
                text.text = numEnemies.ToString();
            else if (text.gameObject.name == "Time")
                text.text = time;
        }

        numPrefabsIn++;
    }
}
