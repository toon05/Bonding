using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class SendTextButton : MonoBehaviour
{
    [SerializeField] GameObject inputField;
    DateTime TodayNow;

    String UserId = "s276099";
    String inputText;

    void Start() {
        
    }

    void Update() {
        TodayNow = DateTime.Now;
        inputText = inputField.GetComponent<InputField>().text;
    }

    public void OnClickEvent() {
        
        if (inputText != null) {

            Debug.Log(UserId);
            Debug.Log(TodayNow);
            Debug.Log(inputText);
        
        }
    }
}
