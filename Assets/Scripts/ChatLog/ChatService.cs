using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatService : MonoBehaviour
{
    [SerializeField] private GameObject ChatLogger;
    [SerializeField] private GameObject TypeWriterText;
    private ChatLogger chatLogger;
    private TypeWriterText typeWriterText;
    private int maxWords = 32;

    // Start is called before the first frame update
    void Start()
    {
        chatLogger = ChatLogger.GetComponent<ChatLogger>();
        typeWriterText = TypeWriterText.GetComponent<TypeWriterText>();
    }

    public void RegisterChat(string speaker, string message)
    {
        Debug.Log("RegisterChat");
        chatLogger.AddMessage(speaker, message);
        if (speaker == "BOT")
        {
            List<string> messageList = MessageSplitter.SplitMessage(message, maxWords);
            typeWriterText.SetText(messageList);
        }
    }
}