using UnityEngine;
using UnityEngine.UI;

public class UpdateChatLog : MonoBehaviour
{
    [SerializeField] private TextToJson textToJson = null;
    [SerializeField] private TextChatWindow textChatWindow = null;

    void Start()
    {
        if (textToJson != null && textChatWindow != null)
        {
            // チャットログをロードし、表示
            textToJson.LoadChatLog();
            textChatWindow.DisplayChatLog(textToJson.ChatLog);
        }
        else
        {
            Debug.LogError("TextToJson または TextChatWindow がアサインされていません。");
        }
    }

    // ボタンがクリックされた時に呼ばれる関数
    public void OnLoadChatLogButtonClicked()
    {
        if (textToJson != null && textChatWindow != null)
        {
            textToJson.LoadChatLog();
            textChatWindow.DisplayChatLog(textToJson.ChatLog);
        }
        else
        {
            Debug.LogError("TextToJson または TextChatWindow がアサインされていません。");
        }
    }
}
