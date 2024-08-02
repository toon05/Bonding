using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class ChatMessage
{
    public string Speaker;
    public string Timestamp;
    public string Message;
}

[Serializable]
public class ChatLog
{
    public List<ChatMessage> Messages = new List<ChatMessage>();
}

public class ChatLogger : MonoBehaviour
{
    private ChatLog chatLog = new ChatLog();
    private string filePath;

    private void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "chatlog.json");
        LoadChatLog();
    }

    public void AddMessage(string speaker, string message)
    {
        ChatMessage newMessage = new ChatMessage
        {
            Speaker = speaker,
            Timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            Message = message
        };

        chatLog.Messages.Add(newMessage);
        SaveChatLog();
    }

    private void SaveChatLog()
    {
        string json = JsonUtility.ToJson(chatLog, true);
        File.WriteAllText(filePath, json);
    }

    public void LoadChatLog()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            chatLog = JsonUtility.FromJson<ChatLog>(json);
        }
        Debug.Log("ろーど");
    }

    // ChatLogにアクセスするためのプロパティ
    public ChatLog ChatLog
    {
        get { return chatLog; }
    }
}