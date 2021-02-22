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

        clientSocket.SendTo(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, endPoint);
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

public class CSNetworkManager : MonoBehaviour
{
    Client client;
    Thread recThread;
    public bool send = false;
    public bool interpetCommands = true;
    public int sessionID = 6969;

    public List<PlayerConfiguration> localPlayers = new List<PlayerConfiguration>();
    List<PlayerConfiguration> remotePlayers = new List<PlayerConfiguration>();
    List<GameObject> tempRemoteMenuPlayers = new List<GameObject>();
    List<GameObject> localPlayersGameObject = new List<GameObject>();

    void Awake()
    {
        client = new Client();
        recThread = new Thread(client.Receive);
        recThread.Start();

        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        client.leave = true;
        var buffer = Encoding.ASCII.GetBytes("endMsg");
        client.clientSocket.SendTo(buffer, client.endPoint);
        recThread.Join();


        client.clientSocket.Close();
    }

    public void AddNetworkedPlayer(PlayerConfiguration config, bool isLocal)
    {
        foreach (var r in remotePlayers)
            if (r == config)
                return;
        foreach (var l in localPlayers)
            if (l == config)
                return;

        if (isLocal)
        {
            localPlayers.Add(config);
            byte[] buffer = Encoding.ASCII.GetBytes("initMsg " + sessionID.ToString());
            client.clientSocket.SendTo(buffer, client.endPoint);
            Debug.Log("Local Player Joined");

            return;
        }

        remotePlayers.Add(config);

    }

    public void SendReadyMessage(PlayerConfiguration config)
    {
        if (config.clientNumber == -1)
            return;
        client.Send("cli " + config.clientNumber.ToString() + " plr ready");//temp, only works for one player rn
        config.sentReadyMessage = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    float timer = 0.0f;
    float changeTimer = 3.0f;
    bool changed = false;

    GamePlayerManager playerManager;
    // Update is called once per frame
    void Update()
    {
        SortRecievedMessages();

        //char select section
        if (changed)
        {
            timer -= Time.deltaTime;

            if (playerManager == null)
                playerManager = FindObjectOfType<GamePlayerManager>();

            if (timer <= 0.0f && send)
            {
                for (int i = 0; i < localPlayers.Count; i++)
                {
                    if (playerManager.players[i].GetComponent<UnityEngine.InputSystem.PlayerInput>().enabled == false)
                        continue;

                    client.Send("cli " + localPlayers[i].clientNumber.ToString() + " plr pos "
                    + playerManager.players[i].transform.position.x.ToString() + " "
                    + playerManager.players[i].transform.position.y.ToString() + " "
                    + playerManager.players[i].transform.position.z.ToString()
                    );
                }

                timer = 0.090f;
            }
        }
        else
            charSelect();
    }

    void charSelect()
    {
        timer -= Time.deltaTime;

        bool allReady = true;
        foreach (var p in localPlayers)
        {
            if (!p.isReady)
                allReady = false;

            if (p.isReady && !p.sentReadyMessage)
                SendReadyMessage(p);
            if (timer <= 0.0f)
            {
                p.sentReadyMessage = false;
                timer = 1.0f;
            }
        }

        foreach (var p in remotePlayers)
        {
            if (!p.isReady)
                allReady = false;
        }


        if (localPlayers.Count == 0 && remotePlayers.Count == 0)
            allReady = false;

        if (!allReady)
            changeTimer = 3.0f;

        else
        {
            changeTimer -= Time.deltaTime;

            if (changeTimer <= 0.0f)
            {
                changed = true;
                PlayerConfigurationManager.get.allPlayersReady();
            }
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

            else if (client.backlog[i].Contains("clin"))
            {
                //temp code, will only work for 1 player
                foreach (var lplayer in localPlayers)
                {
                    if (lplayer.clientNumber == -1)
                    {
                        var parts = client.backlog[i].Split(' ');
                        lplayer.clientNumber = int.Parse(parts[1]);
                        Debug.Log(lplayer.clientNumber);
                        break;
                    }
                }

                client.backlog.RemoveAt(i);
                i--;
                continue;
            }
            else if (client.backlog[i].Contains("spawn"))
            {
                var parts = client.backlog[i].Split(' ');

                var remotePlayer = new PlayerConfiguration(null);
                remotePlayer.clientNumber = int.Parse(parts[1]);
                remotePlayers.Add(remotePlayer);
                PlayerConfigurationManager.get.playerConfigurations.Add(remotePlayer);
                remotePlayer.index = PlayerConfigurationManager.get.playerConfigurations.Count - 1;
                var np = GameObject.Instantiate(PlayerConfigurationManager.get._configPrefab);

                PlayerConfigurationManager.get.setPlayerCharacter(remotePlayer.index, np.GetComponent<MakePlayerCharSelectMenu>().playerSetupMenu.GetComponent<PlayerCharSelectMenu>()._characterPrefabs[1]);
                np.GetComponent<UnityEngine.InputSystem.PlayerInput>().enabled = false;
                DontDestroyOnLoad(np);
                //tempRemoteMenuPlayers.Add(np);
                client.backlog.RemoveAt(i);
                i--;
                continue;
            }
            else if (client.backlog[i].Contains("remove"))
            {
                var parts = client.backlog[i].Split(' ');
                int index = int.Parse(parts[1]);

                for (int j = 0; j < remotePlayers.Count; j++)
                {
                    if (remotePlayers[j].clientNumber == index)
                    {
                        remotePlayers.RemoveAt(j);
                        break;
                    }
                }

                client.backlog.RemoveAt(i);
                i--;
                continue;
            }

            foreach (var p in remotePlayers)
            {
                string comp = "cli " + p.clientNumber.ToString();

                if (client.backlog[i].Contains(comp))
                {
                    if (RunCommand(p, client.backlog[i]))
                    {

                        client.backlog.RemoveAt(i);
                        i--;
                        continue;
                    }
                    else if (playerManager != null)
                    {
                        for (int j = 0; j < playerManager.players.Count; j++)
                        {
                            if(playerManager.players[j].GetComponent<UnityEngine.InputSystem.PlayerInput>().enabled == false)
                                RunCommand(playerManager.players[j],client.backlog[i]);
                        }
                    }
                }

            }

        }
    }

    bool RunCommand(PlayerConfiguration p, string command)
    {
        if (command.Contains("plr") && command.Contains("ready"))
        {
            p.isReady = true;
            return true;
        }

        return false;
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
        }
        return false;
    }
}
