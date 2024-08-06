using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkQuestion : ITalk
{
    public bool HasFinishTalk { get; } = true;
    public bool HasAnalysisTalk { get; } = false;
    public bool HasAllowInput { get; } = false;
    
    public string StartTalk(Chat chat)
    {
        string[] talkThemes =
        {
            "Why is the sky blue?", 
            "Why are the leaves green?"
        };
        
        string talkTheme = talkThemes[Random.Range(0, talkThemes.Length)];
        
        return talkTheme;
    }
}
