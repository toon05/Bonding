using UnityEngine;
using UnityEngine.UI;

public class TouchAndMousePosition : MonoBehaviour
{
    public GameObject handImagePrefab; // hand画像のプレハブ
    private GameObject handInstance;
    [SerializeField] private Canvas canvas;

    void Start()
    {
        // プレハブからhand画像のインスタンスをCanvasの子として作成
        handInstance = Instantiate(handImagePrefab, canvas.transform);
        handInstance.SetActive(false); // 初期状態では非表示にする
    }

    void Update()
    {
        Vector2 screenPosition = Vector2.zero;
        bool shouldDisplay = false;

        // タッチ入力を確認
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // 一番最初の指を取得
            screenPosition = touch.position;
            shouldDisplay = true;
        }
        // マウス入力を確認
        else if (Input.GetMouseButton(0))
        {
            screenPosition = Input.mousePosition;
            shouldDisplay = true;
        }

        if (shouldDisplay)
        {
            // hand画像の位置をスクリーン座標からワールド座標に変換して設定
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPosition, canvas.worldCamera, out Vector2 localPoint);
            handInstance.GetComponent<RectTransform>().localPosition = localPoint;
            handInstance.SetActive(true); // hand画像を表示
        }
        else
        {
            handInstance.SetActive(false); // タッチやマウスが押されていないときはhand画像を非表示にする
        }
    }
}