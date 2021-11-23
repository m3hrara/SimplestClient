using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystemManager : MonoBehaviour
{
    GameObject UsernameInputField, PasswordInputField, UsernameText, PasswordText, SubmitButton, LoginToggle, CreateToggle, JoinGameRoomButton, 
        QuickChatOneButton, QuickChatTwoButton, QuickChatThreeButton,GameScreen,
        MessageInputField, SendMessageButton;
    GameObject NetworkedClient;
    float ExtraHeight;
    Text ChatBoxOne, ChatBoxTwo, ChatBoxThree;
    List<Message> MessageList = new List<Message>();
    public GameObject TextPrefab, ChatBox;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if(go.name == "UsernameInputField")
            {
                UsernameInputField = go;
            }
            else if (go.name == "PasswordInputField")
            {
                PasswordInputField = go;
            }
            else if (go.name == "UsernameText")
            {
                UsernameText = go;
            }
            else if (go.name == "PasswordText")
            {
                PasswordText = go;
            }
            else if (go.name == "SubmitButton")
            {
                SubmitButton = go;
            }
            else if (go.name == "LoginToggle")
            {
                LoginToggle = go;
            }
            else if (go.name == "CreateToggle")
            {
                CreateToggle = go;
            }
            else if (go.name == "NetworkedClient")
            {
                NetworkedClient = go;
            }
            else if (go.name == "JoinGameRoomButton")
            {
                JoinGameRoomButton = go;
            }
            else if (go.name == "QuickChatOneButton")
            {
                QuickChatOneButton = go;
            }
            else if (go.name == "QuickChatTwoButton")
            {
                QuickChatTwoButton = go;
            }
            else if (go.name == "QuickChatThreeButton")
            {
                QuickChatThreeButton = go;
            }
            else if (go.name == "GameScreen")
            {
                GameScreen = go;
            }
            else if (go.name == "MessageInputField")
            {
                MessageInputField = go;
            }
            else if (go.name == "SendMessageButton")
            {
                SendMessageButton = go;
            }
        }
        Text[] allTexts = UnityEngine.Object.FindObjectsOfType<Text>();
        foreach (Text go in allTexts)
        {
            if (go.name == "ChatBoxOne")
            {
                ChatBoxOne = go;
            }
            else if (go.name == "ChatBoxTwo")
            {
                ChatBoxTwo = go;
            }
            else if (go.name == "ChatBoxThree")
            {
                ChatBoxThree = go;
            }
        }
        SubmitButton.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        LoginToggle.GetComponent<Toggle>().onValueChanged.AddListener(LoginToggleChanged);
        CreateToggle.GetComponent<Toggle>().onValueChanged.AddListener(CreateToggleChanged);
        JoinGameRoomButton.GetComponent<Button>().onClick.AddListener(JoinGameRoomButtonPressed);
        QuickChatOneButton.GetComponent<Button>().onClick.AddListener(QuickChatOneButtonPressed);
        QuickChatTwoButton.GetComponent<Button>().onClick.AddListener(QuickChatTwoButtonPressed);
        QuickChatThreeButton.GetComponent<Button>().onClick.AddListener(QuickChatThreeButtonPressed);
        SendMessageButton.GetComponent<Button>().onClick.AddListener(SendMessageButtonPressed);

        ChangeState(gameStates.LoginMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SendMessageButtonPressed()
    {
        string txt = MessageInputField.GetComponent<InputField>().text;
        string msg;
        msg = ClientToServerSignifier.SendMessage + "," + NetworkedClient.GetComponent<NetworkedClient>().Username + "," + txt;
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
    }
    public void PrintMessageToView(string txt)
    {
        ExtraHeight -= 20;
        int max = 8;
        MessageInputField.GetComponent<InputField>().text = "";
        if(MessageList.Count >=max)
        {
            Destroy(MessageList[0].textObject.gameObject);
            MessageList.Remove(MessageList[0]);
        }
        Message msg = new Message(txt);
        GameObject text = Instantiate(TextPrefab, ChatBox.transform);
        text.GetComponent<RectTransform>().anchoredPosition = new Vector2(TextPrefab.GetComponent<RectTransform>().anchoredPosition.x, TextPrefab.GetComponent<RectTransform>().anchoredPosition.y + ExtraHeight);
        msg.textObject = text.GetComponent<Text>();
        msg.textObject.text = " " + msg.text;
        MessageList.Add(msg);
    }
    public void SubmitButtonPressed()
    {
        string p = PasswordInputField.GetComponent<InputField>().text;
        string un = UsernameInputField.GetComponent<InputField>().text;
        string msg;
        if (CreateToggle.GetComponent<Toggle>().isOn)
        {
            msg = ClientToServerSignifier.CreateAccount + "," + un + "," + p;
        }
        else
        {
            msg = ClientToServerSignifier.Login + "," + un + "," + p;
        }
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
    }
    public void LoginToggleChanged(bool changed)
    {
        CreateToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(!changed);
    }
    public void CreateToggleChanged(bool changed)
    {
        LoginToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(!changed);
    }

    public void ChangeState(int newState)
    {
        JoinGameRoomButton.SetActive(false);

        UsernameInputField.SetActive(false);
        PasswordInputField.SetActive(false);
        UsernameText.SetActive(false);
        PasswordText.SetActive(false);
        SubmitButton.SetActive(false);
        LoginToggle.SetActive(false);
        CreateToggle.SetActive(false);
      //  TicTacToeSquareULButton.SetActive(false);
        GameScreen.SetActive(false);

        if (newState == gameStates.LoginMenu)
        {
            UsernameInputField.SetActive(true);
            PasswordInputField.SetActive(true);
            UsernameText.SetActive(true);
            PasswordText.SetActive(true);
            SubmitButton.SetActive(true);
            LoginToggle.SetActive(true);
            CreateToggle.SetActive(true);
        }
        else if (newState == gameStates.MainMenu)
        {
            JoinGameRoomButton.SetActive(true);
        }
        else if (newState == gameStates.WaitingInQueueForOtherPlayer)
        {

        }
        else if (newState == gameStates.TicTacToeGame)
        {
            GameScreen.SetActive(true);
        }
    }

    public void JoinGameRoomButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.JoinQueueForGame + "");
        ChangeState(gameStates.WaitingInQueueForOtherPlayer);
    }

    public void QuickChatOneButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.QuickChatOne + "");
    }
    public void QuickChatTwoButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.QuickChatTwo + "");
    }

    public void QuickChatThreeButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.QuickChatThree + "");
    }

    public void QuickChatOneRecieved()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("Opponent: Good Luck!");
    }
    public void QuickChatTwoRecieved()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("Opponent: Get Ready!");
    }
    public void QuickChatThreeRecieved()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("Opponent: GG!");
    }

    public void QuickChatOneSent()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("You: Good Luck!");
    }
    public void QuickChatTwoSent()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("You: Get Ready!");
    }
    public void QuickChatThreeSent()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("You: GG!");
    }
}

static public class gameStates
{
    public const int LoginMenu = 1;

    public const int MainMenu = 2;

    public const int WaitingInQueueForOtherPlayer = 3;

    public const int TicTacToeGame = 4;
}
[System.Serializable]
public class Message
{
    public Text textObject;
    public string text;
    public Message(string newTxt)
    {
        this.text = newTxt;
    }
}