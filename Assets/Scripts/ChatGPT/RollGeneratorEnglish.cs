using System.Collections.Generic;

public class RollGeneratorEnglish : IRollGenerator
{
    private readonly string _initialText = "You are a character in the game and interact with me as a parent. You are a child, unaware of many things, a pure being.";
    private readonly string[] _growthRateTexts = new[]
    {
        "You are a two-year-old child. You can only speak in two or three word sentences of simple words.", 
        "You are a 3 year old child. You are able to speak simple compound sentences with multiple predicates.", 
        "You are a five-year-old child. You have become good at asserting yourself verbally and telling others what you have experienced."
    };

    private int _growthLevel = 0;
    private string _playerName;
    private string _partnerName;
    private List<string> attributes = new List<string>();

    public RollGeneratorEnglish(int growthlevel, string playerName, string partnerName)
    {
        _growthLevel = growthlevel;
        _playerName = playerName;
        _partnerName = partnerName;
    }
    
    /// <summary>
    /// ロール用のプロンプトを生成する
    /// </summary>
    /// <returns></returns>
    public string GeneratePrompt(int gl)
    {
        _growthLevel = gl;
        string prompt = "";
        prompt += _initialText + "\n";
        prompt += _growthRateTexts[_growthLevel] + "\n";
        prompt += $"I am called {_playerName} and my partner is called {_partnerName}.";
        prompt += "Below are your characteristics. Please speak according to this in 30 characters or less. If you see [INSTRUCTIONS], that is what you will say, so please rewrite that content into the robot's spoken language and speak it to the user.";
        foreach (string at in attributes)
        {
            prompt += $"・{at}\n";
        }

        return prompt;
    }

    /// <summary>
    /// 属性を追加する
    /// </summary>
    /// <param name="attribute"></param>
    public void AddAttribute(string attribute)
    {
        attributes.Add(attribute);
    }
}
