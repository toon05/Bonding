using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AAAAA : MonoBehaviour
{
    public GameObject TextToJson;
    private TextToJson textToJson;
    public GameObject inputText;
    private TMP_InputField inputField;
    private string message;

    void Start()
    {
        if (TextToJson != null)
        {
            textToJson = TextToJson.GetComponent<TextToJson>();
            if (textToJson == null)
            {
                Debug.LogError("TextToJson component not found on TextToJson GameObject.");
            }
        }
        else
        {
            Debug.LogError("TextToJson GameObject is not assigned.");
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
        if (inputField != null && textToJson != null)
        {
            message = inputField.text;
            textToJson.AddMessage("UserID_123", message);
        }
        else
        {
            Debug.LogError("inputField or textToJson is null.");
        }
    }

    public void OnClickOpponentButton()
    {
        if (inputField != null && textToJson != null)
        {
            message = inputField.text;
            textToJson.AddMessage("BOT", message);
        }
        else
        {
            Debug.LogError("inputField or textToJson is null.");
        }
    }
}