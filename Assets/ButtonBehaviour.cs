using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    public Button button;
    public GameObject NetworkedClient;
    private int siblingIndex;
    private bool isTaken;
    void Start()
    {
        isTaken = false;
        NetworkedClient = GameObject.Find("NetworkedClient");
        siblingIndex = transform.GetSiblingIndex();
    }
    public void SetButtonSprite(int playerID)
    {
        isTaken = true;
        button.image.sprite = sprites[playerID];
    }
    public void OnButtonPressed()
    {
        if(!isTaken)
        {
            string msg;
            msg = ClientToServerSignifier.SendButtonIndex + "," + siblingIndex;
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
        }
    }
}
