using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkTopicToPartner : ITalk
{
    public bool HasFinishTalk { get; } = true;
    public bool HasAnalysisTalk { get; } = true;
    public bool HasAllowInput { get; } = false;
    
    public string StartTalk(Chat chat)
    {
        string[] talkThemes =
        {
            "What do you like about your partner?", 
            "What has your partner done for you recently that made you happy?"
        };
        
        string talkTheme = talkThemes[Random.Range(0, talkThemes.Length)];
        
        return talkTheme;
    }
}
