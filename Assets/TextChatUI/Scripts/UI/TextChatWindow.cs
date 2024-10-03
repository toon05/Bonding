using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextChatWindow : MonoBehaviour
{
    public enum CommentType
    {
        Mine,
        Opponent
    }

    [SerializeField] private GameObject myComment = null;
    [SerializeField] private GameObject opponentComment = null;
    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private float maxFieldSize = 300.0f;

    private RectTransform selfRectTransform_ = null;
    private RectTransform scrollRectTransform_ = null;
    private bool panelPreviouslyActive = false;  // パネルの前回の状態

    void Start()
    {
        selfRectTransform_ = this.GetComponent<RectTransform>();
        scrollRectTransform_ = scrollRect.GetComponent<RectTransform>();
    }

    void Update()
    {
        // スクロールビューの高さ調整
        Vector2 offsetMin = scrollRectTransform_.offsetMin;
        offsetMin.y = 0; // 入力フィールドがないので高さは固定
        scrollRectTransform_.offsetMin = offsetMin;
    }

    public void DisplayChatLog(ChatLog chatLog)
    {
        // メッセージリストのサイズを取得
        int messageCount = chatLog.Messages.Count;

        // 直近のメッセージ数を決定（最大20個または全メッセージ数）
        int recentMessageCount = Mathf.Min(20, messageCount);

        // 直近のメッセージを抽出
        List<ChatMessage> recentMessages = chatLog.Messages.GetRange(messageCount - recentMessageCount, recentMessageCount);

        // メッセージを古い順に並べて表示
        foreach (ChatMessage message in recentMessages)
        {
            CommentType commentType = DetermineCommentType(message.Speaker);
            AddComment(commentType, message.Message);
        }
    }

    private CommentType DetermineCommentType(string speaker)
    {
        // スピーカーに応じてコメントタイプを決定
        if (speaker == "BOT") // 自分の名前に変更
        {
            return CommentType.Opponent;
        }
        else
        {
            return CommentType.Mine;
        }
    }

    public void AddComment(CommentType commentType, string message)
    {
        GameObject baseObj = null;
        switch (commentType)
        {
            case CommentType.Mine: baseObj = myComment; break;
            case CommentType.Opponent: baseObj = opponentComment; break;
            default: break;
        }
        if (baseObj == null) { return; }

        GameObject copy = GameObject.Instantiate(baseObj);
        copy.transform.SetParent(baseObj.transform.parent, false);
        copy.SetActive(true);
        copy.GetComponent<SpeechBundle>().SetText(message);
    }
}