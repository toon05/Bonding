using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkTopicFromPartner : ITalk
{
    public bool HasFinishTalk { get; } = true;
    public bool HasAnalysisTalk { get; } = false;
    public bool HasAllowInput { get; } = false;
    
    public string StartTalk(Chat chat)
    {
        string talkTheme = "";
        talkTheme += "The person you are associating with had the following to say.\n";
        talkTheme = chat._talkPartnerThemes[0];
        chat._talkPartnerThemes.RemoveAt(0);
        
        return talkTheme;
    }
}
