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
        if (isLocal)
        {
            localPlayers.Add(config);
            byte[] buffer = Encoding.ASCII.GetBytes("initMsg " + sessionID.ToString());
            client.clientSocket.SendTo(buffer, client.endPoint);
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

    // Update is called once per frame
    void Update()
    {
        SortRecievedMessages();

        foreach (var p in localPlayers)
            if (p.isReady && !p.sentReadyMessage)
                SendReadyMessage(p);
        foreach (var p in remotePlayers)
            if (p.isReady && !p.sentReadyMessage)
                SendReadyMessage(p);
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
                        break;
                    }
                }

                client.backlog.RemoveAt(i);
                i--;
                continue;
            }
            else if (client.backlog[i].Contains("spawn"))
            {
                var remotePlayer = new PlayerConfiguration(null);
                PlayerConfigurationManager.get.playerConfigurations.Add(remotePlayer);
                remotePlayers.Add(remotePlayer);
                GameObject.Instantiate(PlayerConfigurationManager.get._configPrefab);
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

                if (client.backlog[i].Contains(comp) && RunCommand(p, client.backlog[i]))
                {
                    client.backlog.RemoveAt(i);
                    i--;
                    continue;
                }
            }

        }
    }

    bool RunCommand(PlayerConfiguration p, string command)
    {
        if (command.Contains("plr"))
        {
            if (command.Contains("ready"))
            {
                p.isReady = true;
                return true;
            }
        }

        return false;
    }
}
