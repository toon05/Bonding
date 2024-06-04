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
    FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

    string UserId = "s276099";
    string inputText;

    void Start() {
        
    }

    void Update() {
        TodayNow = DateTime.Now;
    }

    public void OnValueChanged(string text) {
        inputText = inputField.GetComponent<TMP_InputField>().text;
        Debug.Log("Input Text: " + inputText);
    }

    public void OnClick() {

        if (inputText != null) {

            Debug.Log(TodayNow);
            DocumentReference docRef = db.Collection("messages").Document("test");
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
