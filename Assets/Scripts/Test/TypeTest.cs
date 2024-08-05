using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeTest : MonoBehaviour
{
    [SerializeField] private GameObject ChatService;
    private ChatService chatService;

    // Start is called before the first frame update
    void Start()
    {
        chatService = ChatService.GetComponent<ChatService>();
        chatService.RegisterChat("BOT", "くぅ～疲れましたw これにて完結です！実は、ネタレスしたら代行の話を持ちかけられたのが始まりでした本当は話のネタなかったのですが←ご厚意を無駄にするわけには行かないので流行りのネタで挑んでみた所存ですw");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
