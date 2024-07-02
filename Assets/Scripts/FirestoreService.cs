using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Threading.Tasks;

public class FirestoreService : MonoBehaviour
{
    public FirebaseFirestore db;
    public Query query;
    public ListenerRegistration listener;
    public DocumentChange result;
    public event Action<DocumentChange, DocumentSnapshot, Metadata> OnFirestoreDataChange;

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
                result = change;
                OnFirestoreDataChange?.Invoke(change, snapshot, snapshot.Metadata);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    /// <summary>
    /// オブジェクトが破壊されるとリスナーを停止
    /// </summary>
    void OnDestroy()
    {
        if (listener != null)
        {
            listener.Stop();
        }
    }

    /// <summary>
    /// テキストを送信
    /// </summary>
    /// <param name="sendText">送信するテキスト</param>
    /// <param name="sendCollectionName">送信先</param>
    /// <param name="userId">送信元</param>
    /// <param name="timeStamp">タイムスタンプ</param>
    public void SendData(string sendText,string sendCollectionName, string userId, string timeStamp)
    {
        DocumentReference docRef = db.Collection(sendCollectionName).Document(userId + "_" + timeStamp);
        Dictionary<string, object> dataMap = new Dictionary<string, object>
        {
            { "userid", userId },
            { "message", sendText },
            { "timestamp", timeStamp }
        };
        docRef.SetAsync(dataMap).ContinueWithOnMainThread(task => {
            Debug.Log("そーしん!!!!");
        });
    }
}