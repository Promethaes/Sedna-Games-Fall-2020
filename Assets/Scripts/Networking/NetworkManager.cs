using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
public class StateObject
{
    public const int BufferSize = 1024;
    public byte[] buffer = new byte[BufferSize];
    public string finalString = "";
    public int index = -1;
    public EndPoint remoteClient;
}
class Client
{
    public Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    public Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    EventWaitHandle sendDone = new EventWaitHandle(true, EventResetMode.ManualReset);
    EventWaitHandle receiveDone = new EventWaitHandle(true, EventResetMode.ManualReset);
    public IPEndPoint endPoint;
    public List<string> backlog = new List<string>();
    public bool leave = false;

    public Client()
    {
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = IPAddress.Parse("192.168.0.46");
        endPoint = new IPEndPoint(ipAddress, 5000);

        clientSocket.Bind(new IPEndPoint(IPAddress.Any, 0));

    }

    public void Send(string message)
    {
        byte[] sendBuffer = Encoding.ASCII.GetBytes(message);

        clientSocket.BeginSendTo(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, endPoint, new AsyncCallback(SendCallback), clientSocket);
    }

    void SendCallback(IAsyncResult ar)
    {
        try
        {
            Socket client = (Socket)ar.AsyncState;

            int length = client.EndSendTo(ar);

            sendDone.Set();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void Receive()
    {
        while (true)
        {
            if (leave)
                break;
            receiveDone.Reset();
            try
            {
                byte[] buffer = new byte[1024];
                var ep = (EndPoint)endPoint;
                int length = clientSocket.ReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref ep);

                var fString = Encoding.ASCII.GetString(buffer, 0, length);

                Console.WriteLine(fString);
                backlog.Add(fString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            receiveDone.Set();
        }
    }
}

public class NetworkManager : MonoBehaviour
{
    Client client;
    Thread recThread;
    public GameObject playerPrefab;
    public bool send = false;
    public bool interpetCommands = true;
    public SceneChanger sceneChanger;
    struct PItem
    {
        public GameObject p;
        public NetworkingMovementScript nMovement;
        public Transform transform;

        public PItem(GameObject pObject)
        {
            p = pObject;
            nMovement = pObject.GetComponent<NetworkingMovementScript>();
            transform = p.gameObject.transform;
        }
    }

    List<PItem> players = new List<PItem>();
    PItem player;

    void Start()
    {
        client = new Client();
        recThread = new Thread(client.Receive);
        recThread.Start();
        byte[] buffer = Encoding.ASCII.GetBytes("initMsg");
        client.clientSocket.SendTo(buffer, client.endPoint);
        player = new PItem(GameObject.Instantiate(playerPrefab));
        player.p.name = "LocalPlayer";
        players.Add(player);

        DontDestroyOnLoad(gameObject);
    }

    public void Send(string message)
    {
        client.Send(message);
    }

    private void OnDestroy()
    {
        client.leave = true;
        var buffer = Encoding.ASCII.GetBytes("endMsg");
        client.clientSocket.SendTo(buffer, client.endPoint);
        recThread.Join();


        client.clientSocket.Close();
    }

    Vector3 lastPos = new Vector3();
    float timer = 0.0f;
    bool sentReadyMessage = false;
    void FixedUpdate()
    {
        if (timer <= 0.0f && send && Mathf.Abs(player.transform.position.magnitude - lastPos.magnitude) >= 0.01f)
        {
            if (player.nMovement.readyPressed && !sentReadyMessage)
            {
                Send("cli " + player.nMovement.networkedPlayerNum.ToString() + "plr ready");
                sentReadyMessage = true;
            }

            Send("cli " + player.nMovement.networkedPlayerNum.ToString() + " plr pos "
            + player.transform.position.x.ToString() + " "
            + player.transform.position.y.ToString() + " "
            + player.transform.position.z.ToString()
            );
            timer = 0.033f;
        }
        SortRecievedMessages();
        lastPos = player.transform.position;
        timer -= Time.fixedDeltaTime;

        bool allReady = true;
        foreach (var p in players)
        {
            allReady = p.nMovement.readyPressed;
        }

        if (allReady)
        {
            foreach (var p in players)
            {
                p.nMovement.readyPressed = false;
            }
            sceneChanger.changeScene(2);//TEMP
        }

    }
    void SortRecievedMessages()
    {
        if (!interpetCommands)
            return;

        for (int i = 0; i < client.backlog.Count; i++)
        {
            if (client.backlog[i].Length >= 40)
            {
                client.backlog.RemoveAt(i);
                i--;
                continue;
            }
            else if (client.backlog[i].Contains("clin") && player.nMovement.networkedPlayerNum == -1)
            {
                var parts = client.backlog[i].Split(' ');
                player.nMovement.networkedPlayerNum = int.Parse(parts[1]);
                client.backlog.RemoveAt(i);
                i--;
                Debug.Log(player.nMovement.networkedPlayerNum);
                continue;
            }
            else if (client.backlog[i].Contains("spawn"))
            {
                var parts = client.backlog[i].Split(' ');
                var temp = new PItem(GameObject.Instantiate(playerPrefab));
                temp.nMovement.networkedPlayerNum = int.Parse(parts[1]);
                temp.nMovement.enabled = false;
                client.backlog.RemoveAt(i);
                i--;
                players.Add(temp);
                Debug.Log("Client " + temp.nMovement.networkedPlayerNum.ToString() + " joined the session");
                continue;
            }
            else if (client.backlog[i].Contains("remove"))
            {
                var parts = client.backlog[i].Split(' ');
                int index = int.Parse(parts[1]);

                for (int j = 0; j < players.Count; i++)
                {
                    if (players[j].nMovement.networkedPlayerNum == j)
                    {
                        Destroy(players[j].p);
                        players.RemoveAt(j);
                        j--;
                        break;
                    }
                }
            }

            foreach (var p in players)
            {
                string comp = "cli" + " " + p.nMovement.networkedPlayerNum.ToString();

                if (client.backlog[i].Contains(comp) && RunCommand(p.p, client.backlog[i]))
                {
                    client.backlog.RemoveAt(i);
                    i--;
                    continue;
                }
            }
        }
    }


    bool RunCommand(GameObject p, string command)
    {
        if (command.Contains("plr"))
        {
            var parts = command.Split(' ');
            Vector3 v = new Vector3(float.Parse(parts[4]), float.Parse(parts[5]), float.Parse(parts[6]));

            if (command.Contains("pos"))
            {
                p.transform.position = v;
                return true;
            }
            else if (command.Contains("scl"))
            {
                p.transform.localScale = v;
                return true;
            }
            else if (command.Contains("vel"))
            {
                var r = p.GetComponent<Rigidbody>();
                r.velocity = r.velocity + v;
                return true;
            }
            else if (command.Contains("ready"))
            {
                var m = p.GetComponent<NetworkingMovementScript>();
                m.readyPressed = true;
                return true;
            }
        }

        Debug.Log("Invalid Command: " + command);
        return false;
    }


}
