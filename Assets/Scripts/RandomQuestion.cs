using System.Collections.Generic;
using UnityEngine;

public class RandomQuestion : MonoBehaviour
{
    [SerializeField] private TalkThemeAsset talkThemeAsset;
    [SerializeField] private GameObject ChatService;
    private ChatService chatService;
    private string[] allQuestions;
    private Dictionary<string, (string key, int index)> questionToKeyAndIndexMap;
    public bool isQuestion = false;
    public string questionKey;
    public int? questionIndex;

    // Start is called before the first frame update
    void Start()
    {
        chatService = ChatService.GetComponent<ChatService>();

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickQuestionButton()
    {
        if (allQuestions.Length > 0)
        {
            isQuestion = true;
            
            // ランダムに質問を選ぶ
            string randomQuestion = allQuestions[Random.Range(0, allQuestions.Length)];
            
            // 選ばれた質問をDebug.Logで表示
            Debug.Log("ランダムな質問: " + randomQuestion);

            // 選ばれた質問が属するキーとインデックスを特定して表示
            if (questionToKeyAndIndexMap.TryGetValue(randomQuestion, out var value))
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

            // 選ばれた質問をChatServiceに登録
            chatService.RegisterChat("BOT", randomQuestion);
        }
        else
        {
            Debug.Log("質問がありません。");
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