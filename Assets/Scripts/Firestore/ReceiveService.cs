using System.Collections.Generic;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;
using System.Threading.Tasks;

public class ReceiveService : MonoBehaviour
{
    FirebaseFirestore db;
    DocumentReference docRef;
    ListenerRegistration listener;

    public int messageCount = 0;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        docRef = db.Collection("System").Document("messages");
        
        // Listenメソッドをここで呼び出す
        listener = docRef.Listen(snapshot =>
        {
            if (snapshot.Exists)
            {
                // ドキュメントのデータを取得
                Dictionary<string, object> data = snapshot.ToDictionary();

                // messagesCountフィールドの値を取得してmessageCountに代入
                if (data.ContainsKey("messagesCount") && data["messagesCount"] is long messagesCountValue)
                {
                    messageCount = (int)messagesCountValue;
                    Debug.Log($"messagesCount: {messageCount}");
                }
                else
                {
                    Debug.LogWarning("messagesCountフィールドが存在しないか、形式が正しくありません。");
                }
            }
            else
            {
                Debug.Log("No messages document found in System collection.");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
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