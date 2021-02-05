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
    EventWaitHandle connectWaitHandle = new EventWaitHandle(true, EventResetMode.ManualReset);
    EventWaitHandle sendDone = new EventWaitHandle(true, EventResetMode.ManualReset);
    EventWaitHandle receiveDone = new EventWaitHandle(true, EventResetMode.ManualReset);
    IPEndPoint endPoint;
    public List<String> backlog = new List<string>();
    public Client()
    {
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = IPAddress.Parse("192.168.0.46");
        endPoint = new IPEndPoint(ipAddress, 5000);


        connectWaitHandle.WaitOne();
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
        try
        {
            StateObject state = new StateObject();
            state.remoteClient = (EndPoint)endPoint;

            clientSocket.BeginReceiveFrom(state.buffer, 0, StateObject.BufferSize, SocketFlags.None, ref state.remoteClient, new AsyncCallback(RecieveCallback), state);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    void RecieveCallback(IAsyncResult ar)
    {
        try
        {
            StateObject state = (StateObject)ar.AsyncState;

            int length = clientSocket.EndReceiveFrom(ar, ref state.remoteClient);

            state.finalString = Encoding.ASCII.GetString(state.buffer, 0, length);
            backlog.Add(state.finalString);

            clientSocket.BeginReceiveFrom(state.buffer, 0, StateObject.BufferSize, SocketFlags.None, ref state.remoteClient, new AsyncCallback(RecieveCallback), state);
            receiveDone.Set();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}

public class NetworkManager : MonoBehaviour
{
    Client client;
    Thread recThread;

    public GameObject player;

    public int clientNum = -1;
    void Start()
    {
        client = new Client();
        recThread = new Thread(client.Receive);
        //listener = new NetworkManagerUDPListener(port,IPAddress.Parse(ip));
        if (recThread.ThreadState == ThreadState.Unstarted)
            recThread.Start();
    }

    public void Send(string message)
    {
        client.Send(message);
    }

    private void OnDestroy()
    {
        // client.Close();
    }

    void Update()
    {
        SortRecievedMessages();
    }

    void SortRecievedMessages()
    {
        for (int i = 0; i < client.backlog.Count; i++)
        {
            if (client.backlog[i].Length >= 40)
            {
                client.backlog.RemoveAt(i);
                i--;
                continue;
            }
            if (client.backlog[i].Contains("clin") && clientNum == -1)
            {
                var parts = client.backlog[i].Split(' ');
                clientNum = int.Parse(parts[1]);
                client.backlog.RemoveAt(i);
                i--;
                Debug.Log(clientNum);
                continue;
            }

            string comp = "cli" + " " + clientNum.ToString();

            if (client.backlog[i].Contains(comp) && RunCommand(client.backlog[i]))
            {
                client.backlog.RemoveAt(i);
                i--;
            }
        }
    }


    bool RunCommand(string command)
    {
        if (command.Contains("plr"))
        {
            var parts = command.Split(' ');
            Vector3 v = new Vector3(float.Parse(parts[4]), float.Parse(parts[5]), float.Parse(parts[6]));

            if (command.Contains("pos"))
            {
                player.transform.position = v;
                return true;
            }
            else if (command.Contains("scl"))
            {
                player.transform.localScale = v;
                return true;
            }
            else if (command.Contains("vel"))
            {
                var r = player.GetComponent<Rigidbody>();
                r.velocity = r.velocity + v;
                return true;
            }
        }

        Debug.Log("Invalid Command: " + command);
        return false;
    }


}
