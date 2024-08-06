using UnityEngine;
using System.Collections.Generic;
using Firebase.Firestore;
using System.Threading.Tasks;
using Firebase.Extensions;

public class SendService : MonoBehaviour
{
    private FirebaseFirestore db;

    void Start()
    {
        // FireStoreのインスタンスを初期化
        db = FirebaseFirestore.DefaultInstance;
    }

    /// <summary>
    /// FireStoreにデータを送信する
    /// </summary>
    /// <param name="sendCollectionName">送信先のコレクション</param>
    /// <param name="documentID">コレクションの中のドキュメントを指定</param>
    /// <param name="dictionary">送信するデータ(辞書型)</param>
    /// <returns></returns>
    public async Task SendData(string sendCollectionName, string documentID, Dictionary<string, object> dictionary)
    {
        try
        {
            // FireStoreの初期化確認
            if (db == null)
            {
                Debug.LogError("FireStore is not initialized. Make sure Firebase is properly set up.");
                return;
            }

            // コレクションとドキュメントの参照を取得
            DocumentReference docRef = db.Collection(sendCollectionName).Document(documentID);

            // データを送信
            await docRef.UpdateAsync(dictionary).ContinueWithOnMainThread(task => {
                if (task.IsFaulted)
                {
                    Debug.LogError($"Error sending data: {task.Exception}");
                }
                else if (task.IsCompleted)
                {
                    Debug.Log($"Data successfully sent to collection: {sendCollectionName}, document: {documentID}");
                }
            });
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error in SendData method: {e.Message}\nStackTrace: {e.StackTrace}");
        }
    }
}