using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Net;
using System.Net.Sockets;
using System.Text;
//https://docs.microsoft.com/en-us/dotnet/framework/network-programming/using-udp-services
public class UDPClient : MonoBehaviour
{
    public string message;
    Socket socket;
    IPAddress broadcast = IPAddress.Parse("127.0.0.1");

    byte[] sendbuf = Encoding.ASCII.GetBytes("HELLO");
    IPEndPoint ep;

    // Start is called before the first frame update
    void Start()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        ep = new IPEndPoint(broadcast, 11000);
        sendbuf = Encoding.ASCII.GetBytes("didnt work lol");
    }

    public void Send(string msg)
    {
        sendbuf = Encoding.ASCII.GetBytes(msg);
        socket.SendTo(sendbuf, ep);
    }

    // Update is called once per frame
    void Update()
    {
    }

    
}
