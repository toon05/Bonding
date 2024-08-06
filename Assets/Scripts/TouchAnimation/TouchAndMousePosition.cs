using UnityEngine;

public class TouchAndMousePosition : MonoBehaviour
{
    void Update()
    {
        // 画面に触れている指の数を取得
        int touchCount = Input.touchCount;

        // 画面に触れている指があるか確認
        if (touchCount > 0)
        {
            // すべての指について処理を行う
            for (int i = 0; i < touchCount; i++)
            {
                // 指の情報を取得
                Touch touch = Input.GetTouch(i);

                // 指の位置を取得
                Vector2 touchPosition = touch.position;

                // 指の位置をデバッグログに出力
                Debug.Log("Touch Position: " + touchPosition);

                // 指の位置をワールド座標に変換
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, 0));
                Debug.Log("World Position: " + worldPosition);

                // 指の位置に基づいて処理を行う
                // ここに具体的な処理を追加
            }
        }

        // 左クリックがホールドされているか確認
        if (Input.GetMouseButton(0))
        {
            // マウスカーソルの位置を取得
            Vector2 mousePosition = Input.mousePosition;

            // マウスカーソルの位置をデバッグログに出力
            Debug.Log("Mouse Position: " + mousePosition);

            // マウスカーソルの位置をワールド座標に変換
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));
            Debug.Log("World Position: " + worldPosition);

            // マウスカーソルの位置に基づいて処理を行う
            // ここに具体的な処理を追加
        }
    }
}