using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatService : MonoBehaviour
{
    [SerializeField] private GameObject TextToJson;
    [SerializeField] private GameObject TypeWriterText;
    [SerializeField] private GameObject BotTalkBox;
    private TextToJson textToJson;
    private TypeWriterText typeWriterText;
    private int maxWords = 50;

    // Start is called before the first frame update
    void Start()
    {
        textToJson = TextToJson.GetComponent<TextToJson>();
        typeWriterText = TypeWriterText.GetComponent<TypeWriterText>();
    }

    public void RegisterChat(string speaker, string message)
    {
        Debug.Log("RegisterChat");
        textToJson.AddMessage(speaker, message);
        if (speaker == "BOT")
        {
            BotTalkBox.SetActive(true);
            List<string> messageList = MessageSplitter.SplitMessage(message, maxWords);
            typeWriterText.SetText(messageList);
        }
    }
}