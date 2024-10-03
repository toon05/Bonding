using UnityEngine;

public class ChatLogViewer : MonoBehaviour
{
    public TextToJson textToJson;  // TextToJsonのインスタンス
    public TextChatWindow textChatWindow;  // TextChatWindowのインスタンス

    private void Start()
    {
        if (textToJson != null && textChatWindow != null)
        {
            ChatLog chatLog = textToJson.ChatLog;
            textChatWindow.DisplayChatLog(chatLog);
        }
        else
        {
            Debug.LogError("TextToJsonまたはTextChatWindowの参照が設定されていません！");
        }
    }
}