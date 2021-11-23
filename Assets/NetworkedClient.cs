using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedClient : MonoBehaviour
{
    public string Username;
    int connectionID;
    int maxConnections = 1000;
    int reliableChannelID;
    int unreliableChannelID;
    int hostID;
    int socketPort = 5478;
    byte error;
    bool isConnected = false;
    int ourClientID;

    GameObject gameSystemManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.GetComponent<GameSystemManager>() != null)
            {
                gameSystemManager = go;
            }
        }
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNetworkConnection();
    }

    private void UpdateNetworkConnection()
    {
        if (isConnected)
        {
            int recHostID;
            int recConnectionID;
            int recChannelID;
            byte[] recBuffer = new byte[1024];
            int bufferSize = 1024;
            int dataSize;
            NetworkEventType recNetworkEvent = NetworkTransport.Receive(out recHostID, out recConnectionID, out recChannelID, recBuffer, bufferSize, out dataSize, out error);

            switch (recNetworkEvent)
            {
                case NetworkEventType.ConnectEvent:
                    Debug.Log("connected.  " + recConnectionID);
                    ourClientID = recConnectionID;
                    break;
                case NetworkEventType.DataEvent:
                    string msg = Encoding.Unicode.GetString(recBuffer, 0, dataSize);
                    ProcessRecievedMsg(msg, recConnectionID);
                    //Debug.Log("got msg = " + msg);
                    break;
                case NetworkEventType.DisconnectEvent:
                    isConnected = false;
                    Debug.Log("disconnected.  " + recConnectionID);
                    break;
            }
        }
    }

    private void Connect()
    {

        if (!isConnected)
        {
            Debug.Log("Attempting to create connection");

            NetworkTransport.Init();

            ConnectionConfig config = new ConnectionConfig();
            reliableChannelID = config.AddChannel(QosType.Reliable);
            unreliableChannelID = config.AddChannel(QosType.Unreliable);
            HostTopology topology = new HostTopology(config, maxConnections);
            hostID = NetworkTransport.AddHost(topology, 0);
            Debug.Log("Socket open.  Host ID = " + hostID);

            connectionID = NetworkTransport.Connect(hostID, "192.168.2.12", socketPort, 0, out error); // server is local on network

            if (error == 0)
            {
                isConnected = true;

                Debug.Log("Connected, id = " + connectionID);

            }
        }
    }

    public void Disconnect()
    {
        NetworkTransport.Disconnect(hostID, connectionID, out error);
    }

    public void SendMessageToHost(string msg)
    {
        byte[] buffer = Encoding.Unicode.GetBytes(msg);
        NetworkTransport.Send(hostID, connectionID, reliableChannelID, buffer, msg.Length * sizeof(char), out error);
    }

    private void ProcessRecievedMsg(string msg, int id)
    {
        Debug.Log("msg recieved = " + msg + ".  connection id = " + id);
        string[] csv = msg.Split(',');
        int Signifier = int.Parse(csv[0]);
        if(Signifier == ServerToClientSignifier.AccountCreationComplete)
        {
            Debug.Log("Account creation complete");
            //Username = csv[1];
            gameSystemManager.GetComponent<GameSystemManager>().ChangeState(gameStates.MainMenu);
        }
        else if (Signifier == ServerToClientSignifier.LoginComplete)
        {
            Debug.Log("Login complete");
            //Username = csv[1];
            gameSystemManager.GetComponent<GameSystemManager>().ChangeState(gameStates.MainMenu);
        }
        else if (Signifier == ServerToClientSignifier.GameStart)
        {
            gameSystemManager.GetComponent<GameSystemManager>().ChangeState(gameStates.TicTacToeGame);
        }
        else if (Signifier == ServerToClientSignifier.OpponentPlay)
        {
            Debug.Log("your opponent played");
        }
        else if (Signifier == ServerToClientSignifier.QuickChatOneRecieved)
        {
            gameSystemManager.GetComponent<GameSystemManager>().QuickChatOneRecieved();
        }
        else if (Signifier == ServerToClientSignifier.QuickChatTwoRecieved)
        {
            gameSystemManager.GetComponent<GameSystemManager>().QuickChatTwoRecieved();
        }
        else if (Signifier == ServerToClientSignifier.QuickChatThreeRecieved)
        {
            gameSystemManager.GetComponent<GameSystemManager>().QuickChatThreeRecieved();
        }
        else if (Signifier == ServerToClientSignifier.QuickChatOneSent)
        {
            gameSystemManager.GetComponent<GameSystemManager>().QuickChatOneSent();
        }
        else if (Signifier == ServerToClientSignifier.QuickChatTwoSent)
        {
            gameSystemManager.GetComponent<GameSystemManager>().QuickChatTwoSent();
        }
        else if (Signifier == ServerToClientSignifier.QuickChatThreeSent)
        {
            gameSystemManager.GetComponent<GameSystemManager>().QuickChatThreeSent();
        }
        else if (Signifier == ServerToClientSignifier.TextMessage)
        {
            gameSystemManager.GetComponent<GameSystemManager>().PrintMessageToView(csv[1]);
        }
    }

    public bool IsConnected()
    {
        return isConnected;
    }
}
public static class ClientToServerSignifier
{
    public const int CreateAccount = 1;
    public const int Login = 2;
    public const int JoinQueueForGame = 3;
    public const int TicTacToeSomethingPlay = 4;
    public const int QuickChatOne = 5;
    public const int QuickChatTwo = 6;
    public const int QuickChatThree = 7;
    public const int SendMessage = 8;

}
public static class ServerToClientSignifier
{
    public const int LoginComplete = 1;
    public const int LoginFailed = 2;
    public const int AccountCreationComplete = 3;
    public const int AccountCreationFailed = 4;
    public const int GameStart = 5;
    public const int OpponentPlay = 6;
    public const int QuickChatOneRecieved = 7;
    public const int QuickChatTwoRecieved = 8;
    public const int QuickChatThreeRecieved = 9;
    public const int QuickChatOneSent = 10;
    public const int QuickChatTwoSent = 11;
    public const int QuickChatThreeSent = 12;
    public const int TextMessage = 13;

}