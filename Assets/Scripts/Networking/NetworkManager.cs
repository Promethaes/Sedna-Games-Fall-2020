using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
public class NetworkManagerUDPListener
{
    Thread listenThread = null;
    EventWaitHandle listenThreadWait = new EventWaitHandle(true, EventResetMode.ManualReset);
    EventWaitHandle mainThreadWait = new EventWaitHandle(true, EventResetMode.ManualReset);
    int port = -1;
    UdpClient listener;
    IPEndPoint groupEP;
    public NetworkManagerUDPListener(int port,IPAddress address)
    {
        listener = new UdpClient(port);
        groupEP = new IPEndPoint(address, port);
        listenThread = new Thread(ListenThreadLoop);
        listenThread.Start();
    }
    void ListenThreadLoop()
    {

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

    void UpdateListener()
    {
        listenThreadWait.Set();
    }
}

public class NetworkManagerUDPClient{
    
}

public class NetworkManager : MonoBehaviour
{
    //Using UDP, at the moment
    // Start is called before the first frame update
    NetworkManagerUDPListener listener;
    public string ip = "0.0.0.0.0";

    public int port = 11000;

    void Start()
    {
        listener = new NetworkManagerUDPListener(port,IPAddress.Parse(ip));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
