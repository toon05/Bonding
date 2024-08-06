using System.Collections.Generic;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;
using System.Threading.Tasks;

public class ReceiveService : MonoBehaviour
{
    FirebaseFirestore db;
    DocumentReference docRef;
    DocumentReference eventDocRef;
    ListenerRegistration listener;
    ListenerRegistration eventListener;

    public int messageCount = 0;
    public bool isBirthday;
    public bool isFirstperson;
    public bool isHonorific;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        docRef = db.Collection("System").Document("messages");
        eventDocRef = db.Collection("System").Document("events");

        // messages ドキュメントのリスナーを設定
        listener = docRef.Listen(snapshot =>
        {
            if (snapshot.Exists)
            {
                Dictionary<string, object> data = snapshot.ToDictionary();

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

        // events ドキュメントのリスナーを設定
        eventListener = eventDocRef.Listen(snapshot =>
        {
            if (snapshot.Exists)
            {
                Dictionary<string, object> data = snapshot.ToDictionary();

                // 各フィールドの値を取得して bool 型の変数に格納
                if (data.ContainsKey("isBirthday") && data["isBirthday"] is bool isBirthdayValue)
                {
                    isBirthday = isBirthdayValue;
                    Debug.Log($"isBirthday: {isBirthday}");
                }

                if (data.ContainsKey("isFirstperson") && data["isFirstperson"] is bool isFirstpersonValue)
                {
                    isFirstperson = isFirstpersonValue;
                    Debug.Log($"isFirstperson: {isFirstperson}");
                }

                if (data.ContainsKey("isHonorific") && data["isHonorific"] is bool isHonorificValue)
                {
                    isHonorific = isHonorificValue;
                    Debug.Log($"isHonorific: {isHonorific}");
                }

                Debug.Log($"Updated values - isBirthday: {isBirthday}, isFirstperson: {isFirstperson}, isHonorific: {isHonorific}");
            }
            else
            {
                Debug.Log("No events document found in System collection.");
            }
        });
    }

    // データから bool 値を取得するヘルパーメソッド
    private bool GetBoolFromData(Dictionary<string, object> data, string key)
    {
        if (data.TryGetValue(key, out object value) && value is bool booleanValue)
        {
            return booleanValue;
        }
        else
        {
            Debug.LogWarning($"Key '{key}' not found or value is not a boolean.");
            return false;
        }
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

        if (eventListener != null)
        {
            eventListener.Stop();
        }
    }
}