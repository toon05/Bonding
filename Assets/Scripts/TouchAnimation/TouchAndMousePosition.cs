using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TouchAndMousePosition : MonoBehaviour
{
    public GameObject handImagePrefab; // hand画像のプレハブ
    public bool isAnimation = false; // アニメーション中かどうか
    private GameObject handInstance;
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform displayArea; // GUIから指定できる範囲を設定するためのRectTransform
    [SerializeField] private GameObject[] panels; // パネルの配列

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

        if (AreAllPanelsInactive())
        {
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
        }

        if (shouldDisplay)
        {
            // hand画像の位置をスクリーン座標からローカル座標に変換して設定
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPosition, canvas.worldCamera, out Vector2 localPoint);

            // 手画像が表示エリア内にあるか確認
            if (IsWithinDisplayArea(localPoint))
            {
                handInstance.GetComponent<RectTransform>().anchoredPosition = localPoint;
                handInstance.SetActive(true); // hand画像を表示
            }
            else
            {
                handInstance.SetActive(false); // 範囲外の場合は非表示
            }
        }
        else
        {
            handInstance.SetActive(false); // タッチやマウスが押されていないときはhand画像を非表示にする
        }

        if (handInstance.activeSelf)
        {
            isAnimation = true;
        }
        else
        {
            isAnimation = false;
        }
    }

    private bool IsWithinDisplayArea(Vector2 localPoint)
    {
        // displayAreaのRectTransformの範囲を確認
        return displayArea.rect.Contains(localPoint);
    }

    bool AreAllPanelsInactive()
    {
        return panels.All(panel => !panel.activeSelf);
    }
}