using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPlayButton : MonoBehaviour
{
    [SerializeField] private GameObject SendService;
    private SendService sendService;

    // Start is called before the first frame update
    void Start()
    {
        sendService = SendService.GetComponent<SendService>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 対応したボタンをクリックすると総メッセージ数を更新する
    public void OnDemoButtonClick(int demoIndex)
    {
        Debug.Log("Demo button clicked: " + demoIndex);
        sendService.SendData("System", "messages", new Dictionary<string, object>
        {
            { "messagesCount", demoIndex - 1 }
        });

        if (demoIndex <= 15)
        {
            sendService.SendSetData("System", "events", new Dictionary<string, object>
            {
                { "birthday", 0 },
                { "firstperson", "" },
                { "honorific", 0 },
                { "isBirthday", false },
                { "isFirstperson", false },
                { "isHonorific", false }
            });
        }
        else if (demoIndex <= 25)
        {
            sendService.SendSetData("System", "events", new Dictionary<string, object>
            {
                { "birthday", 0 },
                { "firstperson", "" },
                { "honorific", 0 },
                { "isBirthday", true },
                { "isFirstperson", false },
                { "isHonorific", false }
            });
            Debug.Log("押された " + demoIndex);
        }
        else if (demoIndex <= 30)
        {
            sendService.SendSetData("System", "events", new Dictionary<string, object>
            {
                { "birthday", 0 },
                { "firstperson", "" },
                { "honorific", 0 },
                { "isBirthday", true },
                { "isFirstperson", true },
                { "isHonorific", false }
            });
        }
    }
}
