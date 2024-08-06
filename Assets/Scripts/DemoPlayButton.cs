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
            { "messagesCount", demoIndex }
        });
    }
}
