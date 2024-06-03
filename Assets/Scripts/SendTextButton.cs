using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SendTextButton : MonoBehaviour
{
    [SerializeField] InputField inputField;

    DateTime TodayNow;
    var db = FirebaseFirestore.DefaultInstance;
    DocumentReference docRef
        = db.Collection("message").Document(UserId + ":" + String(TodayNow));

    String UserId = "s276099";

    void Start() {
        inputField = inputField.GetComponent<InputField>();
    }

    void Update() {
        TodayNow = DateTime.Now;
    }

    void OnClickEvent() {
        
        if (inputField.text != NULL) {

            Dictionary<string, object> message = new Dictionary<string, object>
            {
                {"UserId", UserId},
                {"DateTime", TodayNow},
                {"SendText", inputField.text}
            };
            docRef.SetAsync(message).ContinueWithOnMainThread(task => {
                Debug.Log("送信完了！");
            });
        
        }
    }
}
