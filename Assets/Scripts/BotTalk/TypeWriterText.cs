using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class TypeWriterText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textObj = default;
    [SerializeField] private AudioClip typeSound = null; // タイプライター音のクリップ
    [SerializeField] private AudioSource audioSource = null; // 音を再生するためのAudioSource

    private float _feedTime = 0.1f; // 文字送り時間
    private float _t = 0f;
    private int _visibleLen = 0;
    private int _textLen = 0;
    private List<string> _textList = new List<string>(); // テキストリスト
    private int _currentIndex = 0; // 現在のテキストのインデックス
    public bool isTalking = false;
    
    // テキストリストを設定
    public void SetText(List<string> textList)
    {
        isTalking = true;
        _textList = textList;
        _currentIndex = 0;
        if (_textList.Count > 0)
        {
            ShowText(_textList[_currentIndex]);
        }
    }

    // テキストを表示
    private void ShowText(string text)
    {
        textObj.text = text;
        _textLen = text.Length;
        _visibleLen = 0;
        _t = 0;
        textObj.maxVisibleCharacters = 0; // 表示文字数を０に
    }
    
    private void Update()
    {
        if (_visibleLen < _textLen)
        {
            _t += Time.deltaTime;
            if (_t >= _feedTime)
            {
                _t -= _feedTime;
                _visibleLen++;
                textObj.maxVisibleCharacters = _visibleLen; // 表示を1文字ずつ増やす
                
                // タイプライター音を再生
                if (audioSource != null && typeSound != null)
                {
                    audioSource.PlayOneShot(typeSound);
                }
            }
        }

        // スペースキーを押した時の処理
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_visibleLen < _textLen)
            {
                // アニメーションが完了していない場合、即座に全表示
                _visibleLen = _textLen;
                textObj.maxVisibleCharacters = _visibleLen;
            }
            else
            {
                // 次のテキストを表示
                _currentIndex++;
                if (_currentIndex < _textList.Count)
                {
                    ShowText(_textList[_currentIndex]);
                } else
                {
                    isTalking = false;
                }
            }
        }
    }
}
