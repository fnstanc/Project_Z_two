using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Net;
using System.Net.Sockets;

public class ClientSocket : MonoBehaviour
{

    Socket clientSocket = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            connectServer();
        }
    }

    private string ip = "192.168.1.100";
    private int port = 10000;

    private void connectServer()
    {
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress address = IPAddress.Parse(ip);
        IPEndPoint point = new IPEndPoint(address, port);
        clientSocket.Connect(point);
    }

}
