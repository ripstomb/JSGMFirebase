using Firebase.Database;
using Firebase.Auth;
using UnityEngine;
using TMPro;

public class OnlineUsersPanel : MonoBehaviour
{
    [SerializeField] private GameObject userEntryPrefab;
    [SerializeField] private Transform userListParent;

    private DatabaseReference userStatusDatabaseRef;

    void Start()
    {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthStateChanged;
        HandleAuthStateChanged(this, null);
    }

    void HandleAuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
            userStatusDatabaseRef = FirebaseDatabase.DefaultInstance.GetReference("status/" + userId);

            userStatusDatabaseRef.SetValueAsync("online");

            userStatusDatabaseRef.OnDisconnect().SetValue("offline");

            FirebaseDatabase.DefaultInstance.GetReference("status").ValueChanged += HandleOnlineUsersChanged;
        }
    }

    void HandleOnlineUsersChanged(object sender, ValueChangedEventArgs args)
    {
        foreach (Transform child in userListParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var userSnapshot in args.Snapshot.Children)
        {
            if (userSnapshot.Value.ToString() == "online")
            {
                var userEntry = Instantiate(userEntryPrefab, userListParent);
                userEntry.GetComponentInChildren<TMP_Text>().text = userSnapshot.Key;
            }
        }
    }
}
