using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
public class TCPTestServer : MonoBehaviour
{
    private List<TcpClient> listConnectedClients = new List<TcpClient>(new TcpClient[0]);
    private TcpListener tcpListener;
    /// <summary>
    /// Background thread for TcpServer workload.  
    /// </summary>  
    private Thread tcpListenerThread;
    /// <summary>  
    /// Create handle to connected tcp client.  
    /// </summary>  
    private TcpClient connectedTcpClient;

    static public TCPTestServer S;

    private void ListenForIncommingRequests()
    {
        tcpListener = new TcpListener(IPAddress.Any, 50000);
        tcpListener.Start();
        ThreadPool.QueueUserWorkItem(this.ListenerWorker, null);
    }
    private void ListenerWorker(object token)
    {
        while (tcpListener != null)
        {
            print("Its here");
            connectedTcpClient = tcpListener.AcceptTcpClient();
            listConnectedClients.Add(connectedTcpClient);
            // Thread thread = new Thread(HandleClientWorker);
            // thread.Start(connectedTcpClient);
            ThreadPool.QueueUserWorkItem(this.HandleClientWorker, connectedTcpClient);
        }
    }
    private void HandleClientWorker(object token)
    {
        Byte[] bytes = new Byte[1024];
        using (var client = token as TcpClient)
        using (var stream = client.GetStream())
        {
            Debug.Log("New client connected");
            int length;
            // Read incomming stream into byte arrary.                      
            while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                var incommingData = new byte[length];
                Array.Copy(bytes, 0, incommingData, 0, length);
                // Convert byte array to string message.                          
                string clientMessage = Encoding.ASCII.GetString(incommingData);
                Debug.Log(clientMessage);
                // msg = clientMessage;
            }
            if (connectedTcpClient == null)
            {
                return;
            }
        }
        //  ThreadPool.QueueUserWorkItem(this.SendMessage, connectedTcpClient);
    }
    private void SendMessage(object token, string msg)
    {
        if (connectedTcpClient == null)
        {
            Debug.Log("Problem connectedTCPClient null");
            return;
        }
        var client = token as TcpClient;
        {
            try
            {
                //Thread.Sleep(100);
                NetworkStream stream = client.GetStream();
                if (stream.CanWrite)
                {
                    // Get a stream object for writing.    
                    // Convert string message to byte array.              
                    byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(msg);
                    // Write byte array to socketConnection stream.            
                    stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
                    Debug.Log("Server sent his message - should be received by client");
                }
            }
            catch (SocketException socketException)
            {
                Debug.Log("Socket exception: " + socketException);
                return;
            }
        }
    }
    void Start()
    {
        S = this;
        tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequests));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (TcpClient item in listConnectedClients)
            {
                SendMessage(item, "P\r");
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            foreach (TcpClient item in listConnectedClients)
            {
                SendMessage(item, "S\r");
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            foreach (TcpClient item in listConnectedClients)
            {
                SendMessage(item, "A\r");
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            foreach (TcpClient item in listConnectedClients)
            {
                SendMessage(item, "B\r");
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (TcpClient item in listConnectedClients)
            {
                SendMessage(item, "On\r");
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            foreach (TcpClient item in listConnectedClients)
            {
                SendMessage(item, "Off\r");
            }
        }
    }

       public void MessageAll(string msg)
    {

        foreach (TcpClient item in listConnectedClients)
        {
            SendMessage(item, msg);
        }

    }
    public void openCamera()
    {
        if (listConnectedClients != null)
        {
            print("Length " + listConnectedClients.Count);
        }
        //SendMessage( "open_camera");
        foreach (TcpClient item in listConnectedClients)
        {
            SendMessage(item, "open_camera");
        }
    }
}
