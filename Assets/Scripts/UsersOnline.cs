using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsersOnline : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var databaseRef = FirebaseDatabase.DefaultInstance
        .GetReference("online-users");

        databaseRef.ChildAdded += HandleChildAdded;
        databaseRef.ChildChanged += HandleChildChanged;
        databaseRef.ChildRemoved += HandleChildRemoved;
        databaseRef.ChildMoved += HandleChildMoved; 
    }

    private void HandleChildAdded(object sender, ChildChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        Debug.Log(args.Snapshot.Value + " se ha conectado");
    }
    private void HandleChildRemoved(object sender, ChildChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        Debug.Log(args.Snapshot.Value+" se ha desconectado");
    }
    private void HandleChildMoved(object sender, ChildChangedEventArgs args)
    {
    }
    private void HandleChildChanged(object sender, ChildChangedEventArgs args)
    {
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
