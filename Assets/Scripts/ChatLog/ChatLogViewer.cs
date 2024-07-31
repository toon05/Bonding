using UnityEngine;

public class ChatLogViewer : MonoBehaviour
{
    public ChatLogger chatLogger;  // ChatLoggerのインスタンス
    public TextChatWindow textChatWindow;  // TextChatWindowのインスタンス

    private void Start()
    {
        if (chatLogger != null && textChatWindow != null)
        {
            ChatLog chatLog = chatLogger.ChatLog;
            textChatWindow.DisplayChatLog(chatLog);
        }
        else
        {
            Debug.LogError("ChatLoggerまたはTextChatWindowの参照が設定されていません！");
        }
    }
}