using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Firebase.Firestore;
using Firebase.Extensions;


public class SendTextButton : MonoBehaviour
{
    [SerializeField] GameObject inputField;
    [SerializeField] GameObject firestoreServiceObject;
    FirestoreService firestoreService;
    DateTime todayNow;

    string userId = "s276099";
    string inputText;

    void Start() {
        firestoreService = firestoreServiceObject.GetComponent<FirestoreService>();
    }

    void Update() {
        todayNow = DateTime.Now;
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
        if (inputText != "") {
            firestoreService.SendData(inputText, "messages", userId, todayNow.ToString("yyyyMMddHHmmss"));
        }
    }
}
