using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkTopic : ITalk
{
    public bool HasFinishTalk { get; } = true;
    public bool HasAnalysisTalk { get; } = false;
    public bool HasAllowInput { get; } = false;
    
    public string StartTalk(Chat chat)
    {
        string[] talkThemes =
        {
            "I've been having fun talking to everyone lately."
        };
        
        string talkTheme = talkThemes[Random.Range(0, talkThemes.Length)];
        
        return talkTheme;
    }
}
