using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using EnumStringValue;

public class Chat : MonoBehaviour
{
    [SerializeField] ChatGPTModel chatGptModel;
    [SerializeField, Range(0.0f, 2.0f), Tooltip("回答の自由度を設定します。数が大きいほど毎回異なる回答を、小さいほど毎回同じ回答をします。")] float chatGptTemperature = 1;

    // [SerializeField] private ChatMessageView messageViewTemplete;
    // [SerializeField] private InputField inputField;
    // [SerializeField] private ScrollRect scrollRect;
    // [SerializeField] private Button sendButton;
    // [SerializeField] private Button talkButton;
    [SerializeField] private SendService sendService;
    [SerializeField] private TextToJson textToJson;
    [SerializeField, Tooltip("会話するAIのsystem")] static string systemContent = "In this service where you raise a robot on your smartphone, you are a young robot being raised by a couple. Please respond to our conversations in simple, childish language. If something says [instructions], it is a command from the system, so please rewrite it to fit your worldview and ask the user. Also, please do not speak until you are spoken to or given instructions.";
    [SerializeField, Tooltip("パートナーに共有すべきか否かを判別するAIのsystem")] static string determineContent = "You are a robot raised by a couple. Please decide whether they will be pleased or displeased when I tell them what I have told you. If you should tell them, output “TRUE”; if not, output “FALSE”. For example, output TRUE if “seeing you smile makes me feel better” and FALSE if “I want you to at least do the dishes.";
    
    [SerializeField] string apiKey = "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"; // NOTE: 入力したままコミットやリポジトリの公開などをしないこと

    OpenAIChatCompletionAPI chatCompletionAPI;
    
    private string playerName = "";
    private string partnerName = "";

    private IRollGenerator _rollGenerator;
    private bool _canTalkEvent = false;

    public int TalkCount { get; private set; } // 総会話数
    public List<string> _talkPartnerThemes = new List<string>(); // こちらに伝えるパートナーの情報

    private List<OpenAIChatCompletionAPI.Message> context = new List<OpenAIChatCompletionAPI.Message>();
    // {
    //     new OpenAIChatCompletionAPI.Message(){role = "system", content = systemContent}, 
    // };
    
    ITalk talkBody = new TalkQuestion();

    void Awake()
    {
        // messageViewTemplete.gameObject.SetActive(false);
        // sendButton.onClick.AddListener(OnSendClick);
        // talkButton.onClick.AddListener(OnTalkClick);
        chatCompletionAPI = new OpenAIChatCompletionAPI(apiKey);
        
        // ユーザーの名前などをここで初期化
        SetSystemContent(0, "パパ", "ママ");
        
        // // 総会話数を読み込む
        //
        //
    }

    public void SetSystemContent(int growthLevel, string playerName, string partnerName)
    {
        _rollGenerator = new RollGenerator(growthLevel, playerName, partnerName);
        context.Add(new OpenAIChatCompletionAPI.Message(){role = "system", content = _rollGenerator.GeneratePrompt()});
    }

    /// <summary>
    /// 送信ボタンが押された時の処理
    /// </summary>
    // void OnSendClick()
    // {
    //     // 未入力なら送信しない
    //     if (string.IsNullOrEmpty(inputField.text)) return;
    //     // 送信内容をクラスにまとめる
    //     var message = new OpenAIChatCompletionAPI.Message() { role = "user", content = inputField.text};
    //     // 入力された内容をタイムラインに表示
    //     AppendMessage(message, true);
    //
    //     // パートナーに共有すべきかを判定
    //     if (talkBody.HasAnalysisTalk) DetermineWhetherToShare(inputField.text);
    //
    //     inputField.text = "";
    //     // ChatGPTの返信を待つ
    //     ChatCompletionRequest().Forget();
    // }
    
    /// <summary>
    /// こちらに伝えるパートナーの情報が追加された時に呼ぶ
    /// </summary>
    /// <param name="topic">こちらに伝えるパートナーの情報</param>
    public void AddPartnerThemes(string topic)
    {
        _talkPartnerThemes.Add(topic);
    }

    /// <summary>
    /// 会話ボタンが押された時の処理
    /// </summary>
    private void OnTalkClick()
    {
        int talkCategory = Random.Range(0, 3);

        // イベント発生条件を満たしていれば、イベントの文言を喋る
        if (_canTalkEvent) talkCategory = 3;
        // パートナーの新情報があれば、その文言を喋る
        if (_talkPartnerThemes.Count != 0) talkCategory = 4;
        
        switch (talkCategory)
        {
            case 0:
                // パートナーに共有する可能性のある質問
                talkBody = new TalkTopicToPartner();
                Debug.Log("パートナーに共有する質問モード");
                break; 
            case 1:
                // ただの質問
                talkBody = new TalkQuestion();
                Debug.Log("ただの質問モード");
                break; 
            case 2:
                // 雑談
                talkBody = new TalkTopic();
                Debug.Log("雑談モード");
                break;
            case 3:
                // イベント
                talkBody = new TalkEvent();
                Debug.Log("イベントモード");
                break;
            case 4:
                // パートナーの情報
                talkBody = new TalkTopicFromPartner();
                Debug.Log("パートナーの情報を伝えるモード");
                break;
            default: break;
        }

        string firstMessage = talkBody.StartTalk(this);
        
        Debug.Log("指示：" + firstMessage);

        // 送信内容をクラスにまとめる
        var message = new OpenAIChatCompletionAPI.Message() { role = "user", content = "[INSTRUCTIONS]" + firstMessage};
        // 入力された内容をタイムラインに表示
        AppendMessage(message, false);
        
        // ChatGPTの返信を待つ
        ChatCompletionRequest().Forget();
        
        // 総会話数を1増やす
        TalkCount++;
        //sendService.SendData();
    }
    
    /// <summary>
    /// 話題をAIの口調へ変換する
    /// </summary>
    /// <param name="topic">話題</param>
    /// <returns>AIの口調へ変換された話し言葉</returns>
    public async UniTask<string> ConvertTopicToSpokenLanguage(string topic)
    {
        // 送信内容をクラスにまとめる
        var message = new OpenAIChatCompletionAPI.Message() { role = "user", content = "[INSTRUCTIONS]" + topic};
        context.Add(message);

        // // ChatGPTの返信を待つ
        // ChatCompletionRequest().Forget();
        
        var cancellationToken = this.GetCancellationTokenOnDestroy();

        await UniTask.DelayFrame(1, cancellationToken:cancellationToken);
        // scrollRect.verticalNormalizedPosition = 0;

        var response = await chatCompletionAPI.CreateCompletionRequest(
            new OpenAIChatCompletionAPI.RequestData() { messages = context, model = chatGptModel.GetStringValue(), temperature = chatGptTemperature},
            cancellationToken
        );

        var spokenLanguage = response.choices[0].message;
        await UniTask.DelayFrame(1, cancellationToken:cancellationToken);

        return spokenLanguage.content;
    }

    /// <summary>
    /// ChatGPTの返信を待つ
    /// </summary>
    /// <returns></returns>
    private async UniTask ChatCompletionRequest()
    {
        // // ChatGPTの返信待機中は、送信ボタンを非アクティブにする
        // sendButton.interactable = false;
        // talkButton.interactable = false;

        var cancellationToken = this.GetCancellationTokenOnDestroy();

        await UniTask.DelayFrame(1, cancellationToken:cancellationToken);
        // scrollRect.verticalNormalizedPosition = 0;

        var response = await chatCompletionAPI.CreateCompletionRequest(
            new OpenAIChatCompletionAPI.RequestData() { messages = context, model = chatGptModel.GetStringValue(), temperature = chatGptTemperature},
            cancellationToken
        );

        var message = response.choices[0].message;
        // // ChatGPTの返信をタイムラインに表示
        // AppendMessage(message, true);

        await UniTask.DelayFrame(1, cancellationToken:cancellationToken);
        // scrollRect.verticalNormalizedPosition = 0;

        // if (talkBody.HasFinishTalk)
        // {
        //     // ChatGPTの返信受信後は、送信ボタンをアクティブにする
        //     sendButton.interactable = true;
        //     talkButton.interactable = true;
        // }

        if (talkBody.HasAllowInput)
        {
            // イベントの質問に対して、プレイヤーの入力を受け付ける
            
            
            // 入力された情報を、ロールの属性に追加する
            string attribute = "";
            _rollGenerator.AddAttribute(attribute);
            
            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                { "name", "test" },
            };
            // sendService.SendData("Event", "TestDocument", dictionary);
        }
    }

    /// <summary>
    /// パートナーに共有すべきかを判定する
    /// </summary>
    /// <param name="message"></param>
    private async UniTask DetermineWhetherToShare(string message)
    {
        var cancellationToken = this.GetCancellationTokenOnDestroy();
        
        List<OpenAIChatCompletionAPI.Message> sharedContent = new List<OpenAIChatCompletionAPI.Message>()
        {
            new OpenAIChatCompletionAPI.Message() {role = "system", content = determineContent}, 
            new OpenAIChatCompletionAPI.Message() {role = "user", content = message}
        };
        
        var response = await chatCompletionAPI.CreateCompletionRequest(
            new OpenAIChatCompletionAPI.RequestData() { messages = sharedContent, model = chatGptModel.GetStringValue(), temperature = 0},
            cancellationToken
        );

        bool isShare = response.choices[0].message.content == "TRUE";
        
        Debug.Log("message：" + message);
        Debug.Log("共有するか？：" + response.choices[0].message.content);

        if (isShare)
        {
            // パートナーに共有する
            Debug.Log("パートナーに、今の会話内容を共有します。");
            
            //sendService.SendData();
        }
        else
        {
            // 共有しない
            Debug.Log("今の会話の内容は、パートナーに共有しません。");
        }
    }
    
    /// <summary>
    /// メッセージをロボットの吹き出しに表示する
    /// </summary>
    /// <param name="message">表示するメッセージ</param>
    /// <param name="isShowSpeechBalloon">吹き出しに表示するか否か</param>
    private void AppendMessage(OpenAIChatCompletionAPI.Message message, bool isShowSpeechBalloon)
    {
        // 会話の履歴をリストに追加して保存する
        context.Add(message);

        if (TalkCount == 15 || TalkCount == 25 || TalkCount == 30) _canTalkEvent = true;
        Debug.Log("総会話数：" + TalkCount);

        if (!isShowSpeechBalloon) return;
        
        // 吹き出しを表示する処理
        
        
        
        // ログに追加する処理
        textToJson.AddMessage("BOT", message.content);
        
        
        // var messageView = Instantiate(messageViewTemplete);
        // messageView.gameObject.name = "message";
        // messageView.gameObject.SetActive(true);
        // messageView.transform.SetParent(messageViewTemplete.transform.parent, false);
        // messageView.Role = message.role;
        // messageView.Content = message.content;
    }

    public enum ChatGPTModel
    {
        [StringValue("gpt-3.5-turbo")] gpt_35_turbo, 

        [StringValue("gpt-4o")] gpt_4o, 
        [StringValue("gpt-4-turbo")] gpt_4_turbo, 
        [StringValue("gpt-4-turbo-preview")] gpt_4_turbo_preview, 
        [StringValue("gpt-4-vision-preview")] gpt_4_vision_preview, 
        [StringValue("gpt-4-32k")] gpt_4_32k, 
        [StringValue("gpt-4")] gpt_4, 
        [StringValue("gpt-4o-mini")] gpt_4o_mini, 
        [StringValue("gpt-3.5-turbo-16k")] gpt_35_turbo_16k, 
        [StringValue("gpt-3.5-turbo-instruct")] gpt_35_turbo_instruct
    }
}
