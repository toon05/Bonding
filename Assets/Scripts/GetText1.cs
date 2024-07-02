using UnityEngine;

public class ResultLogger : MonoBehaviour
{
    private FirestoreService firestoreService;

    // Start is called before the first frame update
    void Start()
    {
        // FirestoreServiceクラスのインスタンスを取得
        firestoreService = FindObjectOfType<FirestoreService>();

        // FirestoreServiceクラスのresultを監視し、内容を出力
        firestoreService.OnFirestoreDataChange += OnFirestoreDataChangeHandler;
    }

    // FirestoreServiceクラスのresultが更新された際に呼ばれるイベントハンドラー
    private void OnFirestoreDataChangeHandler(DocumentChange change, DocumentSnapshot snapshot, Metadata metadata)
    {
        // FirestoreServiceクラスのresultの内容を出力
        Debug.Log("Document ID: " + change.Document.Id);
        Debug.Log("Change Type: " + change.Type);
        // 他の必要な情報も必要に応じて出力できます

        // ここで必要な処理を追加することも可能
    }
}