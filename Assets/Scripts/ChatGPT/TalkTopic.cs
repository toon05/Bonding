using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkTopic : ITalk
{
    public bool HasFinishTalk { get; } = true;
    public bool HasAnalysisTalk { get; } = false;
    public bool HasAllowInput { get; } = false;
    
    // 通常の話題
    private readonly string[] _talkNormalThemes =
    {
        "I've been having fun talking to everyone lately."
    };
    
    // 幼い時の話題
    private readonly string[] _talkImmatureThemes =
    {
        
    };
    
    public string StartTalk(Chat chat)
    {
        string talkTheme = "";
        
        // 総会話数が5未満の時は、別のリストを参照して雑談の話題を決定する
        if (chat.TalkCount < 5)
        {
            talkTheme = _talkImmatureThemes[Random.Range(0, _talkImmatureThemes.Length)];
        }
        else
        {
            talkTheme = _talkNormalThemes[Random.Range(0, _talkNormalThemes.Length)];
        }
        
        return talkTheme;
    }
}
