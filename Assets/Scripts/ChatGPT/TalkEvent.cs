using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkEvent : ITalk
{
    public bool HasFinishTalk { get; } = true;
    public bool HasAnalysisTalk { get; } = false;
    public bool HasAllowInput { get; } = true;
    
    public string StartTalk(Chat chat)
    {
        Event[] talkThemes =
        {
            new Event("What is the first person you would like to be called?", Event.InputType.Text), // 一人称は？
            new Event("When is your birthday?", Event.InputType.Number) // 誕生日は？
        };

        int talkNo = 0;
        if (chat.TalkCount >= 15) talkNo = 0;
        if (chat.TalkCount >= 25) talkNo = 1;
        if (chat.TalkCount >= 30) talkNo = 2;
        
        string talkTheme = talkThemes[talkNo].Content;
        
        return talkTheme;
    }
}
