using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FriendsListManager : MonoBehaviour
{
    [SerializeField] private GameObject friendEntryPrefab;
    [SerializeField] private Transform friendListParent;

    void Start()
    {
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        DatabaseReference friendsRef = FirebaseDatabase.DefaultInstance.GetReference("friends/" + userId);
        friendsRef.ValueChanged += HandleFriendsListChanged;
    }

    void HandleFriendsListChanged(object sender, ValueChangedEventArgs args)
    {
        foreach (Transform child in friendListParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var friendSnapshot in args.Snapshot.Children)
        {
            var friendEntry = Instantiate(friendEntryPrefab, friendListParent);
            friendEntry.GetComponentInChildren<TMP_Text>().text = friendSnapshot.Key; // Assuming friendSnapshot.Key is the friend's username
        }
    }
}

