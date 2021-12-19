using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystemManager : MonoBehaviour
{
    int index = 0;
    float counter = 0f;
    float max = 60f;
    bool isReplayed = false;
    GameObject UsernameInputField, PasswordInputField, UsernameText, PasswordText, SubmitButton, LoginToggle, CreateToggle, JoinGameRoomButton, 
        QuickChatOneButton, QuickChatTwoButton, QuickChatThreeButton,GameScreen,
        MessageInputField, SendMessageButton;
    GameObject NetworkedClient;
    Button Button0,Button1, Button2, Button3,Button4,Button5,Button6,Button7,Button8, Replay;
    public List<Button> buttons;
    public Button[] allButtons;
    public List<int> temp;
    float ExtraHeight = -180;
    Text ChatBoxOne, ChatBoxTwo, ChatBoxThree;
    List<Message> MessageList = new List<Message>();
    public GameObject TextPrefab, ChatBox;
    public Sprite spriteX, spriteO;
    // Start is called before the first frame update
    void Start()
    {
        buttons = new List<Button>();
        temp = new List<int>();
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

        foreach (Button go in allButtons)
        {
            if (go.name == "Button0")
            {
                Button0 = go;
            }
            else if (go.name == "Button1")
            {
                Button1 = go;
            }
            else if (go.name == "Button2")
            {
                Button2 = go;
            }
            else if (go.name == "Button3")
            {
                Button3 = go;
            }
            else if (go.name == "Button4")
            {
                Button4 = go;
            }
            else if (go.name == "Button5")
            {
                Button5 = go;
            }
            else if (go.name == "Button6")
            {
                Button6 = go;
            }
            else if (go.name == "Button7")
            {
                Button7 = go;
            }
            else if (go.name == "Button8")
            {
                Button8 = go;
            }
            else if (go.name == "Replay")
            {
                Replay = go;
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
        Button0.GetComponent<Button>().onClick.AddListener(SlotZeroButtonPressed);
        Button1.GetComponent<Button>().onClick.AddListener(SlotOneButtonPressed);
        Button2.GetComponent<Button>().onClick.AddListener(SlotTwoButtonPressed);
        Button3.GetComponent<Button>().onClick.AddListener(SlotThreeButtonPressed);
        Button4.GetComponent<Button>().onClick.AddListener(SlotFourButtonPressed);
        Button5.GetComponent<Button>().onClick.AddListener(SlotFiveButtonPressed);
        Button6.GetComponent<Button>().onClick.AddListener(SlotSixButtonPressed);
        Button7.GetComponent<Button>().onClick.AddListener(SlotSevenButtonPressed);
        Button8.GetComponent<Button>().onClick.AddListener(SlotEightButtonPressed);
        Replay.GetComponent<Button>().onClick.AddListener(ReplayButtonPressed);


        ChangeState(gameStates.LoginMenu);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isReplayed)
        {
            counter++;
            if (counter == max)
            {
                if (temp[index] == 1)
                {
                    allButtons[index].image.sprite = spriteX;

                }
                else if (temp[index] == 2)
                {
                    allButtons[index].image.sprite = spriteO;

                }
                index++;
                counter = 0;
                if (index >= 9)
                {
                    isReplayed = false;
                    index = 0;
                }
            }
        }

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
        ExtraHeight += 20;
        int maxHeight = 0;
        MessageInputField.GetComponent<InputField>().text = "";
        Message msg = new Message(txt);
        GameObject text = Instantiate(TextPrefab, ChatBox.transform);
        if(MessageList.Count>0)
        {
            for (int i = 0; i < MessageList.Count; i++)
            {
                MessageList[i].textObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(MessageList[i].textObject.GetComponent<RectTransform>().anchoredPosition.x, MessageList[i].textObject.GetComponent<RectTransform>().anchoredPosition.y + 20);
            }
        }
        if(ExtraHeight >= maxHeight)
        {
            Destroy(MessageList[0].textObject.gameObject);
            MessageList.Remove(MessageList[0]);
        }

        text.GetComponent<RectTransform>().anchoredPosition = new Vector2(TextPrefab.GetComponent<RectTransform>().anchoredPosition.x, TextPrefab.GetComponent<RectTransform>().anchoredPosition.y-160);
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
        else if (newState == gameStates.Observer)
        {
            GameScreen.SetActive(true);
        }
    }

    public void JoinGameRoomButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.JoinQueueForGame + "");
        ChangeState(gameStates.WaitingInQueueForOtherPlayer);
    }
    public void SlotZeroButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonClick + ","+ 0);
    }
    public void SlotOneButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonClick + "," + 1);
    }
    public void SlotTwoButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonClick + "," + 2);
    }
    public void SlotThreeButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonClick + "," + 3);
    }
    public void SlotFourButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonClick + "," + 4);
    }
    public void SlotFiveButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonClick + "," + 5);
    }
    public void SlotSixButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonClick + "," + 6);
    }
    public void SlotSevenButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonClick + "," + 7);
    }
    public void SlotEightButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonClick + "," + 8);
    }

    public void ReplayGame(int playerID, int slot)
    {
        for (int i = 0; i < 9; i++)
        {
            allButtons[i].image.sprite = null;
        }
        temp[slot] = playerID;

        isReplayed = true;
    }
    public void SlotClick(int playerID, int index)
    {
        if(allButtons[index].image.sprite==null)
        {
            if(playerID==1)
            {
                allButtons[index].image.sprite = spriteX;
            }
            else if (playerID == 2)
            {
                allButtons[index].image.sprite = spriteO;
            }
        }
    }
    public void ReplayButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendReplayButton + "");
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
        PrintMessageToView("Opponent: Good Luck!");
    }
    public void QuickChatTwoRecieved()
    {
        PrintMessageToView("Opponent: Get Ready!");
    }
    public void QuickChatThreeRecieved()
    {
        PrintMessageToView("Opponent: GG!");
    }

    public void QuickChatOneSent()
    {
        PrintMessageToView("You: Good Luck!");
    }
    public void QuickChatTwoSent()
    {
        PrintMessageToView("You: Get Ready!");
    }
    public void QuickChatThreeSent()
    {
        PrintMessageToView("You: GG!");
    }
    public void ReplayMoves()
    {
        for(int i=0;i<9;i++)
        {
            temp.Add(-1);
        }
        for(int i=0; i<9;i++)
        {

            if (buttons[i].image.sprite == spriteO)
                {
                    temp[i] = 0;
                }
                else if (buttons[i].image.sprite == spriteX)
                {
                    temp[i] = 1;
                }
            }
            foreach (Button button in buttons)
            {
                button.image.sprite = null;
            }
        isReplayed = true;  

    }
    }

static public class gameStates
{
    public const int LoginMenu = 1;

    public const int MainMenu = 2;

    public const int WaitingInQueueForOtherPlayer = 3;

    public const int TicTacToeGame = 4;

    public const int Observer = 5;
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