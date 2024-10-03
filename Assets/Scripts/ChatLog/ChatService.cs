using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatService : MonoBehaviour
{
    [SerializeField] private GameObject TextToJson;
    [SerializeField] private GameObject TypeWriterText;
    [SerializeField] private GameObject BotTalkBox;
    [SerializeField] private GameObject BotNamePlate;
    [SerializeField] private GameObject QuestionButton;
    [SerializeField] private GameObject RandomQuestion;
    private TextToJson textToJson;
    private TypeWriterText typeWriterText;
    private RandomQuestion randomQuestion;
    private int maxWords = 50;

    // Start is called before the first frame update
    void Start()
    {
        textToJson = TextToJson.GetComponent<TextToJson>();
        typeWriterText = TypeWriterText.GetComponent<TypeWriterText>();
        randomQuestion = RandomQuestion.GetComponent<RandomQuestion>();
    }

    void Update()
    {
        if (typeWriterText.isTalking == false)
        {
            if (!randomQuestion.isQuestion)
            {
                AfterTalk();
            }
        }
    }

    public void RegisterChat(string speaker, string message)
    {
        Debug.Log("RegisterChat");
        textToJson.AddMessage(speaker, message);
        if (speaker == "BOT")
        {
            // ネームプレート消して、テキストボックス表示
            QuestionButton.SetActive(false);
            BotNamePlate.SetActive(false);
            BotTalkBox.SetActive(true);
            List<string> messageList = MessageSplitter.SplitMessage(message, maxWords);
            typeWriterText.SetText(messageList);
        }
    }

    public void AfterTalk()
    {
        QuestionButton.SetActive(true);
        BotNamePlate.SetActive(true);
        BotTalkBox.SetActive(false);
    }
}