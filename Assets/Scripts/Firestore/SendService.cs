using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System;

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
    /// <param name="collectionName">送信先(events, relations)</param>
    /// <param name="userId">送信元</param>
    /// <param name="timeStamp">タイムスタンプ</param>
    public void SendData(string sendText, string collectionName, string userId)
    {
        string timeStamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
        DocumentReference docRef = db.Collection(collectionName).Document(userId + "_" + timeStamp);
        Dictionary<string, object> dataMap = new Dictionary<string, object>
        {
            { "userid", userId },
            { "timestamp", timeStamp },
            { "message", sendText }
        };
        docRef.SetAsync(dataMap).ContinueWithOnMainThread(task => {
            Debug.Log("そーしん!!!!");
        });
    }
}