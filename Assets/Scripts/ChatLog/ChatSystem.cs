using UnityEngine;
using UnityEngine.UI;

public class ChatSystem : MonoBehaviour
{
    [SerializeField] private ChatLogger chatLogger = null;
    [SerializeField] private TextChatWindow textChatWindow = null;

    void Start()
    {
        if (chatLogger != null && textChatWindow != null)
        {
            // チャットログをロードし、表示
            chatLogger.LoadChatLog();
            textChatWindow.DisplayChatLog(chatLogger.ChatLog);
        }
        else
        {
            Debug.LogError("ChatLogger または TextChatWindow がアサインされていません。");
        }
    }

    // ボタンがクリックされた時に呼ばれる関数
    public void OnLoadChatLogButtonClicked()
    {
        if (chatLogger != null && textChatWindow != null)
        {
            chatLogger.LoadChatLog();
            textChatWindow.DisplayChatLog(chatLogger.ChatLog);
        }
        else
        {
            Debug.LogError("ChatLogger または TextChatWindow がアサインされていません。");
        }
    }
}
