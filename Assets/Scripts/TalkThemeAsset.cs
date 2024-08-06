using UnityEngine;

// CreateAssetMenu属性を使用することで`Assets > Create > ScriptableObjects > CreasteEnemyParamAsset`という項目が表示される
// それを押すとこの`EnemyParamAsset`が`QuestionData`という名前でアセット化されてassetsフォルダに入る
[CreateAssetMenu(fileName = "QuestionData", menuName = "ScriptableObjects/CreateQuestionListAsset")]
public class TalkThemeAsset : ScriptableObject
{
    // データ群の先頭をstringにして名前等に設定するとInspectorで見たときに項目TOPに表示されるので見やすくなります。
    public string Title = "ランダム質問一覧";

    // privateでも[SerializeField]をつけることでInspectorで確認できるようになります。
    [SerializeField]
    // 辞書型で格納
    public QuestionList[] QuestionList;
}

[System.Serializable]
public class QuestionList
{
    // 質問のテーマ
    public string key;
    // 質問のリスト
    public string[] Questions;
}