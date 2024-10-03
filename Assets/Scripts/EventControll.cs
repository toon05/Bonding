using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.UI;

public class EventControll : MonoBehaviour
{
    [SerializeField] private GameObject ReceiveService;
    [SerializeField] private GameObject SendService;
    private ReceiveService receiveService;
    private SendService sendService;

    [SerializeField] private GameObject BirthdayInputField;
    [SerializeField] private GameObject FirstPersonInputField;
    [SerializeField] private Slider HonorificSlider;

    private TMP_InputField birthdayInputField;
    private TMP_InputField firstPersonInputField;
    private Slider honorificSlider;

    [SerializeField] private GameObject BirthdayPanel;
    [SerializeField] private GameObject FirstPersonPanel;
    [SerializeField] private GameObject HonorificPanel;

    [SerializeField] private GameObject RandomQuestion;
    private RandomQuestion randomQuestion;

    // Start is called before the first frame update
    void Start()
    {
        receiveService = ReceiveService.GetComponent<ReceiveService>();
        sendService = SendService.GetComponent<SendService>();
        randomQuestion = RandomQuestion.GetComponent<RandomQuestion>();

        // 入力フィールドやスライダーの参照を取得
        birthdayInputField = BirthdayInputField.GetComponent<TMP_InputField>();
        firstPersonInputField = FirstPersonInputField.GetComponent<TMP_InputField>();
        honorificSlider = HonorificSlider.GetComponent<Slider>();
    }

    // 誕生日を送信
    public async void OnClickBirthdayButton()
    {
        string birthday = birthdayInputField.text;
        if (!string.IsNullOrEmpty(birthday))
        {
            sendService.SendData("System", "events", new Dictionary<string, object>
            {
                { "birthday", birthday },
                { "isBirthday", true }
            });
        }
        BirthdayPanel.SetActive(false);
        randomQuestion.isQuestion = false;
    }

    // 一人称を送信 
    public async void OnClickFirstPersonButton()
    {
        string firstPerson = firstPersonInputField.text;
        if (!string.IsNullOrEmpty(firstPerson))
        {
            sendService.SendData("System", "events", new Dictionary<string, object>
            {
                { "firstperson", firstPerson },
                { "isFirstperson", true }
            });
        }
        FirstPersonPanel.SetActive(false);
        randomQuestion.isQuestion = false;
    }

    // 敬語の度合いを送信
    public async void OnClickHonorificButton()
    {
        float honorificValue = honorificSlider.value;
        sendService.SendData("System", "events", new Dictionary<string, object>
        {
            { "honorific", honorificValue },
            { "isHonorific", true }
        });
        HonorificPanel.SetActive(false);
        randomQuestion.isQuestion = false;
    }

    // コレクションから指定したキーの値を取得するメソッド
    public async Task<bool> GetValueFromDocument(string collectionName, string documentID, string key)
    {
        Dictionary<string, object> data = await receiveService.GetDataAsync(collectionName, documentID);

        if (data != null)
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
        Debug.LogWarning("Data is null.");
        return false;
    }
}
