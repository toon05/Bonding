using System.Collections.Generic;

public class RollGenerator : IRollGenerator
{
    private readonly string _initialText = "あなたはゲーム内のキャラクターで、わたしを親に見立てて対話をします。あなたは子どもで多くの物事を知らない、純粋な存在です。";
    private readonly string[] _growthRateTexts = new[]
    {
        "あなたは2歳の子どもです。まだ簡単な単語の二語文や三語文でしか喋ることができません。", 
        "あなたは3歳の子どもです。複数の述語が組み合わさった簡単な複文を話せるようになりました。", 
        "あなたは5歳の子どもです。言葉による自己主張や、体験したことを相手に伝えることが上手になりました。"
    };

    private int _growthLevel = 0;
    private string _playerName;
    private string _partnerName;
    private List<string> attributes = new List<string>();

    public RollGenerator(int growthlevel, string playerName, string partnerName)
    {
        _growthLevel = growthlevel;
        _playerName = playerName;
        _partnerName = partnerName;
    }
    
    /// <summary>
    /// ロール用のプロンプトを生成する
    /// </summary>
    /// <returns></returns>
    public string GeneratePrompt()
    {
        string prompt = "";
        prompt += _initialText + "\n";
        prompt += _growthRateTexts[_growthLevel] + "\n";
        prompt += $"私のことを「{_playerName}」とよび、\nそのパートナーのことを「{_partnerName}」と呼びます。";
        prompt += "以下はあなたの特徴です。これに従って30文字以内で喋ってください。なお、文頭に[INSTRUCTIONS]と表記されている場合には、それがあなたの話す内容ですので、その内容をロボットの話し言葉に書き換えて、ユーザーに喋りかけてください。";
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
