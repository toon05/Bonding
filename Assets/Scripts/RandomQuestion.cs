using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;


public class RandomQuestion : MonoBehaviour
{
    [SerializeField] private TalkThemeAsset talkThemeAsset;
    [SerializeField] private GameObject ChatService;
    [SerializeField] private GameObject ReceiveService;
    [SerializeField] private GameObject Chat;
    private ChatService chatService;
    private ReceiveService receiveService;
    private Chat chat;
    private string[] allQuestions;
    private Dictionary<string, (string key, int index)> questionToKeyAndIndexMap;
    public bool isQuestion = false;
    public string questionKey;
    public int? questionIndex;

    [SerializeField] private GameObject birthdayPanel;
    [SerializeField] private GameObject firstPersonPanel;
    [SerializeField] private GameObject honorificPanel;

    [SerializeField] private Button questionButton;

    [SerializeField] private GameObject EventControll;
    private EventControll eventControll;

    // Start is called before the first frame update
    void Start()
    {
        chatService = ChatService.GetComponent<ChatService>();
        receiveService = ReceiveService.GetComponent<ReceiveService>();
        chat = Chat.GetComponent<Chat>();
        eventControll = EventControll.GetComponent<EventControll>();
        AssignOnClickMethod(OnClickQuestionButton);

        if (talkThemeAsset != null)
        {
            // 全てのQuestionsリストを集める
            allQuestions = CollectAllQuestions(talkThemeAsset);

            // 質問とキーおよびインデックスのマッピングを作成
            questionToKeyAndIndexMap = CreateQuestionToKeyAndIndexMap(talkThemeAsset);
        }
        else
        {
            Debug.LogError("TalkThemeAsset is not assigned.");
        }
    }

    void Update()
    {
        Debug.Log($"messageCount: {receiveService.messageCount}");
        Debug.Log($"isBirthday: {receiveService.isBirthday}, isFirstperson: {receiveService.isFirstperson}, isHonorific: {receiveService.isHonorific}");

        if (receiveService.messageCount == 15 && receiveService.isBirthday == false)
        {
            Debug.Log("Assigning OnClickEventButton");
            AssignOnClickMethod(OnClickEventButton);
        }
        else if (receiveService.messageCount == 25 && receiveService.isFirstperson == false)
        {
            Debug.Log("Assigning OnClickEventButton");
            AssignOnClickMethod(OnClickEventButton);
        }
        else if (receiveService.messageCount == 30 && receiveService.isHonorific == false)
        {
            Debug.Log("Assigning OnClickEventButton");
            AssignOnClickMethod(OnClickEventButton);
        }
        else
        {
            Debug.Log("Assigning OnClickQuestionButton");
            AssignOnClickMethod(OnClickQuestionButton);
        }
    }


    // ボタンの OnClick イベントにメソッドを割り当てるメソッド
    public void AssignOnClickMethod(UnityAction action)
    {
        // 既存のリスナーを全て削除
        questionButton.onClick.RemoveAllListeners();
        // 新しいリスナーを追加
        questionButton.onClick.AddListener(action);
    }

    // 質問ボタンがクリックされた時の処理
    public async void OnClickQuestionButton()
    {
        if (allQuestions.Length > 0)
        {
            isQuestion = true;
            
            // ランダムに質問を選ぶ
            string randomTopic = allQuestions[Random.Range(0, allQuestions.Length)];
            
            // 選ばれた質問をDebug.Logで表示
            Debug.Log("ランダムな質問: " + randomTopic);

            // 選ばれた質問が属するキーとインデックスを特定して表示
            if (questionToKeyAndIndexMap.TryGetValue(randomTopic, out var value))
            {
                string key = value.key;
                int index = value.index;
                Debug.Log("この質問のキー: " + key);
                Debug.Log("質問のインデックス: " + index);
                questionKey = key;
                questionIndex = index;
            }
            else
            {
                Debug.LogError("質問のキーが見つかりません。");
            }

            string randomQuestion = await chat.ConvertTopicToSpokenLanguage(randomTopic);

            // 選ばれた質問をChatServiceに登録
            chatService.RegisterChat("BOT", randomQuestion);
        }
        else
        {
            Debug.Log("質問がありません。");
        }

        Debug.Log(receiveService.isBirthday);
    }

    // イベントボタンがクリックされた時の処理
    public void OnClickEventButton()
    {
        isQuestion = true;
        Debug.Log("イベントボタンがクリックされました。");
        if (receiveService.messageCount <= 15)
        {
            Debug.Log("Assigning Birthday Panel");
            chatService.RegisterChat("BOT", "あなたの誕生日を\nおしえて！");
            birthdayPanel.SetActive(true);
        }
        else if (receiveService.messageCount == 25)
        {
            Debug.Log("Assigning FirstPerson Panel");
            chatService.RegisterChat("BOT", "一人称どうしよう");
            firstPersonPanel.SetActive(true);
        }
        else if (receiveService.messageCount == 30)
        {
            Debug.Log("Assigning Honorific Panel");
            chatService.RegisterChat("BOT", "敬語とか使った方がいいかな？");
            honorificPanel.SetActive(true);
        }
    }


    // 全てのQuestionsリストを一つの配列に集める
    private string[] CollectAllQuestions(TalkThemeAsset asset)
    {
        var questionsList = new List<string>();

        foreach (var questionList in asset.QuestionList)
        {
            questionsList.AddRange(questionList.Questions);
        }

        return questionsList.ToArray();
    }

    // 質問とキーおよびインデックスのマッピングを作成
    private Dictionary<string, (string key, int index)> CreateQuestionToKeyAndIndexMap(TalkThemeAsset asset)
    {
        var map = new Dictionary<string, (string key, int index)>();

        foreach (var questionList in asset.QuestionList)
        {
            for (int i = 0; i < questionList.Questions.Length; i++)
            {
                map[questionList.Questions[i]] = (questionList.key, i);
            }
        }

        return map;
    }
}