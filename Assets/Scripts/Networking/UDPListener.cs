using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

//https://docs.microsoft.com/en-us/dotnet/framework/network-programming/using-udp-services
//https://80.lv/articles/simple-multithreading-for-unity/
public class UDPListener : MonoBehaviour
{
    Thread listenThread = null;
    EventWaitHandle listenThreadWait = new EventWaitHandle(true, EventResetMode.ManualReset);
    EventWaitHandle mainThreadWait = new EventWaitHandle(true, EventResetMode.ManualReset);
    private const int lPort = 11000;
    // Start is called before the first frame update
    UdpClient listener;
    IPEndPoint groupEP;
    void Start()
    {
        listener = new UdpClient(lPort);
        groupEP = new IPEndPoint(IPAddress.Any, lPort);
        listenThread = new Thread(ListenThreadLoop);
        listenThread.Start();
    }

    void ListenThreadLoop()
    {
        listenThreadWait.Reset();
        listenThreadWait.WaitOne();

        while (true)
        {
            listenThreadWait.Reset();

           try
           {
               byte[] bytes = listener.Receive(ref groupEP);

               Debug.Log($" {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");
           }
           catch (SocketException e)
           {
               Debug.Log(e);
           }
           finally
           {
               
           }

            WaitHandle.SignalAndWait(mainThreadWait, listenThreadWait);
        }
    }

    // Update is called once per frame
    void Update()
    {
        listenThreadWait.Set();
    }
}
