using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System;

public class RoadTextButton : MonoBehaviour
{
    FirebaseFirestore db;
    Query allMessagesQuery;

    // Start is called before the first frame update
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        allMessagesQuery = db.Collection("messages");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("RoadTextButton OnClick");
        allMessagesQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allMessagesQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allMessagesQuerySnapshot.Documents)
            {
                Debug.Log(String.Format("Document data for {0} document:", documentSnapshot.Id));
                Dictionary<string, object> message = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in message)
                {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                }
            }
        });
    }
}