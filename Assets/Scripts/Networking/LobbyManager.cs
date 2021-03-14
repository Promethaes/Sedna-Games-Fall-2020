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
        StartCoroutine("SendInfoRequest");
    }

    IEnumerator SendInfoRequest()
    {
        while (true)
        {
            _lobbyClient.Send("roominfo");
            yield return new WaitForSeconds(roomRequestRefreshRate);
        }
    }

    void OnDestroy()
    {
        StopCoroutine("SendInfoRequest");
        _lobbyClient.leave = true;
        recThread.Abort();

        _lobbyClient.clientSocket.Close();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _lobbyClient.backlog.Count; i++)
        {
            for (int j = 0; j < rooms.Count; j++)
            {
                Destroy(rooms[j]);
                rooms.RemoveAt(j);
                j--;
            }
            Debug.Log(_lobbyClient.backlog[i]);

            if (_lobbyClient.backlog[i].Contains("room"))
            {
                var parts = _lobbyClient.backlog[i].Split(' ');
                var newRoom = GameObject.Instantiate(roomPrefab);
                newRoom.GetComponent<SceneNavigationScript>().roomButtonSID = int.Parse(parts[1]);
                newRoom.GetComponent<SceneNavigationScript>().roomOccupents = int.Parse(parts[2]);
                newRoom.GetComponent<LobbyBarTextSetter>().roomOccupents.text = parts[2] + "/4";
                newRoom.transform.SetParent(gameObject.transform);
                rooms.Add(newRoom);
                newRoom.transform.localPosition = new Vector3(roomBarStartPos.x, roomBarStartPos.y - barSpacing * rooms.Count - 1, newRoom.transform.position.z);
                _lobbyClient.backlog.RemoveAt(i);
                i--;
            }
            else if (_lobbyClient.backlog[i] == "no")
            {
                Debug.Log("Clearing rooms");
                for (int j = 0; j < rooms.Count; j++)
                {
                    Destroy(rooms[j]);
                    rooms.RemoveAt(j);
                    j--;
                }
            }

        }
    }
}
