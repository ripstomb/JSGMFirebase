using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendStatusManager : MonoBehaviour
{
    void Start()
    {
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        DatabaseReference friendsRef = FirebaseDatabase.DefaultInstance.GetReference("friends/" + userId);

        friendsRef.ValueChanged += (sender, args) =>
        {
            foreach (var friendSnapshot in args.Snapshot.Children)
            {
                string friendId = friendSnapshot.Key;
                DatabaseReference friendStatusRef = FirebaseDatabase.DefaultInstance.GetReference("status/" + friendId);
                friendStatusRef.ValueChanged += HandleFriendStatusChanged;
            }
        };
    }

    void HandleFriendStatusChanged(object sender, ValueChangedEventArgs args)
    {
        string friendId = ((DatabaseReference)sender).Key;
        string status = args.Snapshot.Value.ToString();
        Debug.Log($"Friend {friendId} is now {status}");
    }
}

