using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendRequestsManager : MonoBehaviour
{
    [SerializeField] private GameObject requestEntryPrefab;
    [SerializeField] private Transform requestListParent;
    [SerializeField] private TMP_Text User;
    [SerializeField] private Button Accept;
    [SerializeField] private Button Remove;

    private DatabaseReference friendRequestDatabaseRef;

    void Start()
    {
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        friendRequestDatabaseRef = FirebaseDatabase.DefaultInstance.GetReference("friendRequests/" + userId);
        friendRequestDatabaseRef.ChildAdded += HandleFriendRequestReceived;
    }
    void HandleFriendRequestReceived(object sender, ChildChangedEventArgs args)
    {
        var requestEntry = Instantiate(requestEntryPrefab, requestListParent);
        requestEntry.GetComponentInChildren<TMP_Text>(User).text = args.Snapshot.Value.ToString();
        requestEntry.GetComponentInChildren<Button>(Accept).onClick.AddListener(() => AcceptFriendRequest(args.Snapshot.Key, args.Snapshot.Value.ToString()));
    }

    void AcceptFriendRequest(string requestId, string friendUserId)
    {
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        DatabaseReference friendsRef = FirebaseDatabase.DefaultInstance.GetReference("friends/" + userId);
        friendsRef.Child(friendUserId).SetValueAsync(true);

        friendsRef = FirebaseDatabase.DefaultInstance.GetReference("friends/" + friendUserId);
        friendsRef.Child(userId).SetValueAsync(true);

        friendRequestDatabaseRef.Child(requestId).RemoveValueAsync();
    }

    public void SendFriendRequest(string friendUserId)
    {
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        DatabaseReference newRequestRef = FirebaseDatabase.DefaultInstance.GetReference("friendRequests/" + friendUserId).Push();
        newRequestRef.SetValueAsync(userId);
    }
}
