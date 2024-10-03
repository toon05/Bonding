using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using Unity.Collections;

public class SendQuestion : MonoBehaviour
{
    [SerializeField] private GameObject InputField;
    [SerializeField] private GameObject RandomQuestion;
    [SerializeField] private GameObject ChatService;
    [SerializeField] private GameObject SendService;
    [SerializeField] private GameObject ReceiveService;
    [SerializeField] private Chat chat;
    private TMP_InputField inputField;
    private RandomQuestion randomQuestion;
    private ChatService chatService;
    private SendService sendService;
    private ReceiveService receiveService;

    private int messagesCount;

    [SerializeField] private string UserID = "User1";

    // Start is called before the first frame update
    void Start()
    {
        inputField = InputField.GetComponent<TMP_InputField>();
        randomQuestion = RandomQuestion.GetComponent<RandomQuestion>();
        chatService = ChatService.GetComponent<ChatService>();
        sendService = SendService.GetComponent<SendService>();
        receiveService = ReceiveService.GetComponent<ReceiveService>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void OnClickSendButton()
    {
        if (inputField == null || randomQuestion == null || chatService == null || sendService == null || receiveService == null)
        {
            Debug.LogError("One or more required components are not assigned.");
            return;
        }

        string sendText = inputField.text;
        if (sendText != "")
        {
            Debug.Log("質問: " + sendText);
            chatService.RegisterChat("User1", sendText);

            if (randomQuestion.isQuestion)
            {
                if (randomQuestion.questionKey == "relation")
                {
                    Debug.Log("関係性の質問です.");

                    string questionIndexString = randomQuestion.questionIndex.HasValue ? randomQuestion.questionIndex.Value.ToString() : string.Empty;

                    Dictionary<string, object> sendDictionary = new Dictionary<string, object>
                    {
                        { questionIndexString, sendText }
                    };

                    sendService.SendData("System", UserID, sendDictionary);
                }

                chatService.AfterTalk();
                randomQuestion.questionKey = "";
                randomQuestion.questionIndex = null;
                randomQuestion.isQuestion = false;
            }

            inputField.text = "";
            
            // ↓柴谷追加==========================================================================
            int a = Random.Range(0, 3);
            if (a == 0)
            {
                string AIreply = await chat.TalkWithAI(sendText);
                chatService.RegisterChat("BOT", AIreply);
                Debug.Log("AIが返答します");
            }
            // ==================================================================================

            // GetDataAsyncメソッドからデータを取得
            Dictionary<string, object> messages = await receiveService.GetDataAsync("System", "messages");

            if (messages == null)
            {
                Debug.LogError("GetDataAsyncメソッドがnullを返しました。");
                return;
            }

            if (messages.TryGetValue("messagesCount", out object countValue))
            {
                if (countValue is long countLong)
                {
                    messagesCount = (int)countLong;
                }
                else if (countValue is int countInt)
                {
                    messagesCount = countInt;
                }
                else if (countValue is string countString && int.TryParse(countString, out int parsedCount))
                {
                    messagesCount = parsedCount;
                }
                else
                {
                    Debug.LogError("messagesCountの型が予想外です: " + countValue.GetType());
                    return;
                }
            }
            else
            {
                Debug.LogError("messagesCountが見つかりません。");
            }

            Debug.Log("メッセージ数: " + messagesCount);

            messagesCount++;
            Dictionary<string, object> updateDictionary = new Dictionary<string, object>
            {
                { "messagesCount", messagesCount }
            };

            sendService.SendData("System", "messages", updateDictionary);
            Debug.Log("メッセージ数を更新しました。");
        }
    }
}