using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardMetricsManager : MonoBehaviour
{
    Client client;
    public int enemiesDefeated = 0;
    public CSNetworkManager nm;

    // Start is called before the first frame update
    void Start()
    {
        client = new Client(nm.IPADDRESS);
        if (!nm.isHostClient)
        {
            Debug.Log("LeaderboardMetricsManager not on host client - disabling...");
            gameObject.SetActive(false);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        SendLeaderboardStats();
        client.leave = true;

        client.clientSocket.Close();
    }
    public void SendLeaderboardStats()
    {
        float seconds = Time.time;
        int minutes = 0;
        int hours = 0;

        while (seconds >= 60.0f)
        {
            seconds /= 60.0f;
            minutes++;
        }

        while (minutes >= 60)
        {
            minutes /= 60;
            hours++;
        }

        seconds = Mathf.Floor(seconds);

        string timeTaken = hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString();

        string fstring = PlayerPrefs.GetString("pid","Player") + " ldr defeated " + enemiesDefeated.ToString() + " " + timeTaken;
        client.Send(fstring);
    }
}
