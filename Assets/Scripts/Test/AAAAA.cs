using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AAAAA : MonoBehaviour
{
    public GameObject ChatLogger;
    private ChatLogger chatLogger;
    public GameObject inputText;
    private TMP_InputField inputField;
    private string message;

    void Start()
    {
        if (ChatLogger != null)
        {
            chatLogger = ChatLogger.GetComponent<ChatLogger>();
            if (chatLogger == null)
            {
                Debug.LogError("ChatLogger component not found on ChatLogger GameObject.");
            }
        }
        else
        {
            Debug.LogError("ChatLogger GameObject is not assigned.");
        }

        if (inputText != null)
        {
            inputField = inputText.GetComponent<TMP_InputField>();
            if (inputField == null)
            {
                Debug.LogError("InputField component not found on inputText GameObject.");
            }
        }
        else
        {
            Debug.LogError("inputText GameObject is not assigned.");
        }
    }

    public void OnClickMineButton()
    {
        if (inputField != null && chatLogger != null)
        {
            message = inputField.text;
            chatLogger.AddMessage("UserID_123", message);
        }
        else
        {
            Debug.LogError("inputField or chatLogger is null.");
        }
    }

    public void OnClickOpponentButton()
    {
        if (inputField != null && chatLogger != null)
        {
            message = inputField.text;
            chatLogger.AddMessage("BOT", message);
        }
        else
        {
            Debug.LogError("inputField or chatLogger is null.");
        }
    }
}