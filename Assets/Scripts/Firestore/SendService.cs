using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

public class SendService : MonoBehaviour
{
    public FirebaseFirestore db;

    // Start is called before the first frame update
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    /// <summary>
    /// テキストを送信
    /// </summary>
    /// <param name="sendText">送信するテキスト</param>
    /// <param name="sendCollectionName">送信先</param>
    /// <param name="userId">送信元</param>
    /// <param name="timeStamp">タイムスタンプ</param>
    public void SendData(string sendText, string sendCollectionName, string userId, string timeStamp)
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