using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatService : MonoBehaviour
{
    [SerializeField] private GameObject TextToJson;
    [SerializeField] private GameObject TypeWriterText;
    [SerializeField] private GameObject BotTalkBox;
    [SerializeField] private GameObject BotNamePlate;
    private TextToJson textToJson;
    private TypeWriterText typeWriterText;
    private int maxWords = 50;

    // Start is called before the first frame update
    void Start()
    {
        textToJson = TextToJson.GetComponent<TextToJson>();
        typeWriterText = TypeWriterText.GetComponent<TypeWriterText>();
    }

    void Update()
    {
        if (typeWriterText.isTalking == false)
        {
            BotNamePlate.SetActive(true);
            BotTalkBox.SetActive(false);
        }
    }

    public void RegisterChat(string speaker, string message)
    {
        Debug.Log("RegisterChat");
        textToJson.AddMessage(speaker, message);
        if (speaker == "BOT")
        {
            // ネームプレート消して、テキストボックス表示
            BotNamePlate.SetActive(false);
            BotTalkBox.SetActive(true);
            List<string> messageList = MessageSplitter.SplitMessage(message, maxWords);
            typeWriterText.SetText(messageList);
        }
    }
}