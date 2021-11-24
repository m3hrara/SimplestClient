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
    Button Button,Button2,Button3,Button4,Button5,Button6,Button7,Button8,Button9;
    float ExtraHeight = -180;
    Text ChatBoxOne, ChatBoxTwo, ChatBoxThree;
    List<Message> MessageList = new List<Message>();
    public GameObject TextPrefab, ChatBox;
    public Sprite spriteX, spriteO;
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

        Button[] allButtons = UnityEngine.Object.FindObjectsOfType<Button>();
        foreach (Button go in allButtons)
        {
            if (go.name == "Button")
            {
                Button = go;
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
            else if (go.name == "Button9")
            {
                Button9 = go;
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
        Button.GetComponent<Button>().onClick.AddListener(SlotOneButtonPressed);
        Button2.GetComponent<Button>().onClick.AddListener(SlotTwoButtonPressed);
        Button3.GetComponent<Button>().onClick.AddListener(SlotThreeButtonPressed);
        Button4.GetComponent<Button>().onClick.AddListener(SlotFourButtonPressed);
        Button5.GetComponent<Button>().onClick.AddListener(SlotFiveButtonPressed);
        Button6.GetComponent<Button>().onClick.AddListener(SlotSixButtonPressed);
        Button7.GetComponent<Button>().onClick.AddListener(SlotSevenButtonPressed);
        Button8.GetComponent<Button>().onClick.AddListener(SlotEightButtonPressed);
        Button9.GetComponent<Button>().onClick.AddListener(SlotNineButtonPressed);

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
    }

    public void JoinGameRoomButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.JoinQueueForGame + "");
        ChangeState(gameStates.WaitingInQueueForOtherPlayer);
    }

    public void SlotOneButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonOne + "");
    }
    public void SlotTwoButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonTwo + "");
    }
    public void SlotThreeButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonThree + "");
    }
    public void SlotFourButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonFour + "");
    }
    public void SlotFiveButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonFive + "");
    }
    public void SlotSixButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonSix + "");
    }
    public void SlotSevenButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonSeven + "");
    }
    public void SlotEightButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonEight + "");
    }
    public void SlotNineButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonNine + "");
    }

    public void SlotOneButtonX()
    {
        if(Button.image.sprite==null)
        Button.image.sprite = spriteX;
    }
    public void SlotOneButtonO()
    {
        if (Button.image.sprite == null)
            Button.image.sprite = spriteO;
    }

    public void SlotTwoButtonX()
    {
        if (Button2.image.sprite == null)
            Button2.image.sprite = spriteX;
    }
    public void SlotTwoButtonO()
    {
        if (Button2.image.sprite == null)
            Button2.image.sprite = spriteO;
    }

    public void SlotThreeButtonX()
    {
        if (Button3.image.sprite == null)
            Button3.image.sprite = spriteX;
    }
    public void SlotThreeButtonO()
    {
        if (Button3.image.sprite == null)
            Button3.image.sprite = spriteO;
    }

    public void SlotFourButtonX()
    {
        if (Button4.image.sprite == null)
            Button4.image.sprite = spriteX;
    }
    public void SlotFourButtonO()
    {
        if (Button4.image.sprite == null)
            Button4.image.sprite = spriteO;
    }

    public void SlotFiveButtonX()
    {
        if (Button5.image.sprite == null)
            Button5.image.sprite = spriteX;
    }
    public void SlotFiveButtonO()
    {
        if (Button5.image.sprite == null)
            Button5.image.sprite = spriteO;
    }

    public void SlotSixButtonX()
    {
        if (Button6.image.sprite == null)
            Button6.image.sprite = spriteX;
    }
    public void SlotSixButtonO()
    {
        if (Button6.image.sprite == null)
            Button6.image.sprite = spriteO;
    }

    public void SlotSevenButtonX()
    {
        if (Button7.image.sprite == null)
            Button7.image.sprite = spriteX;
    }
    public void SlotSevenButtonO()
    {
        if (Button7.image.sprite == null)
            Button7.image.sprite = spriteO;
    }

    public void SlotEightButtonX()
    {
        if (Button8.image.sprite == null)
            Button8.image.sprite = spriteX;
    }
    public void SlotEightButtonO()
    {
        if (Button8.image.sprite == null)
            Button8.image.sprite = spriteO;
    }

    public void SlotNineButtonX()
    {
        if (Button9.image.sprite == null)
            Button9.image.sprite = spriteX;
    }
    public void SlotNineButtonO()
    {
        if (Button9.image.sprite == null)
            Button9.image.sprite = spriteO;
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