using UnityEngine;
using DG.Tweening;
using TMPro;  // TextMeshProを使用する場合

public class tweenTest : MonoBehaviour
{
    public TextMeshProUGUI text;  // TextMeshProUGUIを使用する場合
    public float duration = 0.5f;  // アニメーションの持続時間
    public int dateNum = 2;

    void Start()
    {
        if (text == null)
        {
            Debug.LogError("Text component is not assigned.");
            return;
        }
    }

    // スペースキーを押したとき
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (dateNum == 2) {
                TextAnimation();
                dateNum++;
            } else if (dateNum == 3) {
                // オブジェクトを削除する
                Destroy(text.gameObject);
            }
        }
    }

    void TextAnimation()
    {
        // サイズの変更
        text.transform.DOScale(0.6f, duration).SetEase(Ease.OutQuad);

        // アルファ値の変更
        text.DOFade(0.6f, duration).SetEase(Ease.OutQuad);

        // 位置の変更
        text.rectTransform.DOAnchorPosY(text.rectTransform.anchoredPosition.y + 100f, duration).SetEase(Ease.OutQuad);
    }

    void OnDestroy()
    {
        // オブジェクトが破棄されるときにTweenを停止する
        DOTween.Kill(text);
    }
}
