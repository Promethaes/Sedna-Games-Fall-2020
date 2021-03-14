using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class LobbyManager : MonoBehaviour
{
    public string serverIP = "";
    Client _lobbyClient = null;
    Thread recThread = null;
    public GameObject roomPrefab;
    public List<GameObject> rooms = new List<GameObject>();
    public Vector2 roomBarStartPos;
    public float roomRequestRefreshRate = 2.0f;
    public float barSpacing = 100.0f;

    void Start()
    {
        _lobbyClient = new Client(serverIP);
        recThread = new Thread(_lobbyClient.Receive);
        recThread.Start();
    }

    void OnDestroy()
    {
        _lobbyClient.leave = true;
        recThread.Abort();

        _lobbyClient.clientSocket.Close();
    }

    public void Refresh()
    {
        _lobbyClient.Send("roominfo");
        for (int j = 0; j < rooms.Count; j++)
        {
            Destroy(rooms[j]);
            rooms.RemoveAt(j);
            j--;
        }
    }

    void Update()
    {
        for (int i = 0; i < _lobbyClient.backlog.Count; i++)
        {
            if (_lobbyClient.backlog[i].Contains("room"))
            {
                var parts = _lobbyClient.backlog[i].Split(' ');
                var newRoom = GameObject.Instantiate(roomPrefab);
                newRoom.GetComponent<SceneNavigationScript>().roomButtonSID = int.Parse(parts[1]);
                newRoom.GetComponent<SceneNavigationScript>().roomOccupents = int.Parse(parts[2]);
                newRoom.GetComponent<LobbyBarTextSetter>().roomOccupents.text = parts[2] + "/4";
                newRoom.GetComponent<LobbyBarTextSetter>().roomName.text = "Room " + parts[1];
                newRoom.transform.SetParent(gameObject.transform);
                rooms.Add(newRoom);
                newRoom.transform.localPosition = new Vector3(roomBarStartPos.x, roomBarStartPos.y - barSpacing * rooms.Count - 1, newRoom.transform.position.z);
                _lobbyClient.backlog.RemoveAt(i);
                i--;
            }
            else if (_lobbyClient.backlog[i] == "no")
            {
                for (int j = 0; j < rooms.Count; j++)
                {
                    Destroy(rooms[j]);
                    rooms.RemoveAt(j);
                    j--;
                }
                _lobbyClient.backlog.RemoveAt(i);
                i--;
            }

        }
    }

}
