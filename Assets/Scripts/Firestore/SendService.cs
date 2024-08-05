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
    public void SendData(string collectionName, Dictionary<string, object> dictionary)
    {
        db.Collection(collectionName).AddAsync(dictionary).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Document added with ID: " + task.Result.Id);
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Error adding document: " + task.Exception);
            }
        });
    }
}