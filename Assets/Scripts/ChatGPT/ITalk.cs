using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITalk
{
    string StartTalk(Chat chat); // 会話を始める
    bool HasFinishTalk { get; } // GPTからの返信後に、ユーザーからのメッセージの送信を許可するか
    bool HasAnalysisTalk { get; } // パートナーに共有すべきかを分析するか
    bool HasAllowInput { get; } // GPTからの返信後に、プレイヤーからの入力を許可するか
}
