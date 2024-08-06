1. チャットの登録

ChatService.RegisterChatを呼び出してください。
話者が"BOT"とそれ以外で処理を分けてます。
RegisterChatを呼び出せば、
・フキダシのアクティブ化
・ログへの会話内容の登録
・音出しながら一文字ずつ表示
等全部やってくれます。

呼び出し方の例：TypeTest.csみてね

===================================================

2. FireStoreへの送信
SendService.SendDataを呼び出してください。
以下引数の説明

・sendCollectionName
送信先のコレクションを指定
テスト用以外はすべて"System"にしてます。

・documentID
コレクションの中のどのファイルに書き込むかみたいなやつ
events
->イベントのフラグと結果の管理用

messages
->総会話数管理用
　int型のmessageCountのみ存在してます

relations_A, relations_B
->関係性に関する質問の答え格納用
　AとBで二人の回答を格納します。
　キーは41～52の数字になってて仕様書の番号に対応。

===================================================

3. FireStoreからの受信
ReceiveService.GetDataAsyncを呼び出してください。
戻り値は辞書型です。
以下引数の説明

collectionName
documentID
送信と同じ。

asyncなので、呼び出すときは↓みたいな感じで

Dictionary<string, object> messages = await receiveService.GetDataAsync("System", "messages");

===================================================
3. ランダム質問
質問内容はAssets直下にあるQuestionDataという名前のScriptableObjectで管理してます。
keyの値が、
childhood : 幼少期用の質問
smallTalk : 雑談
question  : 質問
relation  : 関係性に関する質問
にそれぞれ対応。

一旦仕様書に書いてあるテキストをそのまま使ってるのですごい無機質になってます。
質問のテーマからAIに質問を考えさせるのであれば、string RandomQuestionをAIに食わせてください。

RandomQuestion.OnClickQuestionButtonで実装してます。
フキダシをボタンで実装したのでOnClickです。