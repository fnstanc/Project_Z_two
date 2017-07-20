using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

public class ClientSocket
{

    Socket clientSocket = null;

    public ClientSocket()
    {
        connectServer();
    }

    private string ip = "192.168.1.100";
    private int port = 10000;

    private void connectServer()
    {
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress address = IPAddress.Parse(ip);
        IPEndPoint point = new IPEndPoint(address, port);
        clientSocket.Connect(point);
        Thread th = new Thread(ReceiveClient);
        th.IsBackground = true;
        th.Start(clientSocket);
    }

    //接受客户端消息
    private void ReceiveClient(object socket)
    {
        Socket clientPeer = socket as Socket;
        byte[] buff = new byte[1024 * 1024];
        while (true)
        {
            int n = clientPeer.Receive(buff);
            if (n == 0)
                break;
            string str = Encoding.UTF8.GetString(buff, 0, n);
            checkClientMsg(str);
        }
    }

    private void checkClientMsg(string msg)
    {
        Debug.Log("接受到消息:" + msg);
        DDOLObj.Instance.msgs.Enqueue(msg);
    }

    //接口 发送消息
    public void clientSendMsg(byte[] buff)
    {
        clientSocket.Send(buff);
    }

    public void closeSocket()
    {
        this.clientSocket.Close();
    }
}
