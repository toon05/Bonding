using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendTestButton : MonoBehaviour
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

    public void OnClick ()
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object>
        {
            { "name", "test" },
            { "age", 20 }
        };
        sendService.SendData("TestCollection", "TestDocument", dictionary);
        Debug.Log("テスト送信");
    }
}
