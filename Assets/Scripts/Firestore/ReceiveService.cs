using System.Collections.Generic;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;
using System.Threading.Tasks;

public class ReceiveService : MonoBehaviour
{
    FirebaseFirestore db;
    Query query;
    ListenerRegistration listener;

    // Start is called before the first frame update
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        query = db.Collection("messages").OrderBy("Timestamp").Limit(10);
        
        // Listenメソッドをここで呼び出す
        listener = query.Listen(snapshot =>
        {
            foreach (DocumentChange change in snapshot.GetChanges())
            {
                if (change.ChangeType == DocumentChange.Type.Added)
                {
                    Debug.Log(string.Format("New message: {0}", change.Document.Id));
                    DisplayMessage(change.Document.ToDictionary());
                }
                else if (change.ChangeType == DocumentChange.Type.Modified)
                {
                    Debug.Log(string.Format("Modified message: {0}", change.Document.Id));
                    DisplayMessage(change.Document.ToDictionary());
                }
                else if (change.ChangeType == DocumentChange.Type.Removed)
                {
                    Debug.Log(string.Format("Removed message: {0}", change.Document.Id));
                }
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisplayMessage(Dictionary<string, object> messageData)
    {
        // メッセージデータをUIに反映する処理
        string messageText = messageData["Message"].ToString();
        Timestamp timestamp = (Timestamp)messageData["Timestamp"];
        string time = timestamp.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss");

        // ここでUIにメッセージを表示する処理を実装
        Debug.Log($"新着: {messageText} at {time}");
        // 例: messageTextObject.text = $"{time}: {messageText}";
    }

    // コレクションからデータを取得する非同期メソッド
    public async Task<Dictionary<string, object>> GetDataAsync(string collectionName, string documentID)
    {
        if (db == null)
        {
            Debug.LogError("Firestore is not initialized.");
            return null;
        }

        var documentRef = db.Collection(collectionName).Document(documentID);
        DocumentSnapshot document = await documentRef.GetSnapshotAsync();

        if (document.Exists)
        {
            var documentData = document.ToDictionary();
            Debug.Log($"Document data for {document.Id}: {documentData}");
            return documentData;
        }
        else
        {
            Debug.Log("Document does not exist.");
            return null;
        }
    }

    void OnDestroy()
    {
        // オブジェクトが破棄されるときにリスナーを解除する
        if (listener != null)
        {
            listener.Stop();
        }
    }
}