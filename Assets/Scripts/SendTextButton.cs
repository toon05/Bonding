using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Firebase.Firestore;
using Firebase.Extensions;


public class SendTextButton : MonoBehaviour
{
    [SerializeField] GameObject inputField;
    DateTime TodayNow;
    FirebaseFirestore db;

    string UserId = "s276099";
    string inputText;

    void Start() {
        db = FirebaseFirestore.DefaultInstance;
    }

    void Update() {
        TodayNow = DateTime.Now;
    }

    /// <summary>
    /// テキストフィールドの値を取得
    /// </summary>
    public void OnValueChanged(string text) {
        inputText = inputField.GetComponent<TMP_InputField>().text;
        // Debug.Log("Input Text: " + inputText);
    }

    /// <summary>
    /// 送信ボタンを押下したときにテキストが入力されていれば送信
    /// </summary>
    public void OnClick() {

        if (inputText != null) {

            // Debug.Log(TodayNow);

            // Document("test")の名前をメッセージ毎に変更
            DocumentReference docRef = db.Collection("messages").Document(UserId + "_" + TodayNow.ToString("yyyyMMddHHmmssfff"));
            Dictionary<string, object> message = new Dictionary<string, object>
            {
                    { "UserId", UserId },
                    { "Message", inputText },
                    { "Timestamp", TodayNow }
            };
            docRef.SetAsync(message).ContinueWithOnMainThread(task => {
                Debug.Log("送信完了!");
            });
        
        }
    }
}
