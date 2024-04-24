using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using System;
using System.Linq;

public class Leaderboard : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject scoreEntryPrefab;

    private List<ScoreEntry> leaderboard;

    [SerializeField]
    int VerticalOffset = 338;
    int VerticalSpace = 45;
    void Start()
    {
        leaderboard = new List<ScoreEntry>();

        FirebaseDatabase.DefaultInstance
        .GetReference("users").OrderByChild("score").LimitToLast(3)
        .ValueChanged += HandleValueChanged;
        //GetUsersHighestScores();
    }

    private void HandleValueChanged(object sender, ValueChangedEventArgs args) 
    {
        Debug.Log("ValueChanged");
        if (args.DatabaseError != null)
        {
            Debug.Log(args.DatabaseError.Message);
            return;
        }

        DataSnapshot snapshot = args.Snapshot;

        int i = 0;

        var _leaderboard = gameObject.GetComponentsInChildren<ScoreEntry>();
        foreach (var go in _leaderboard)
        {
            Destroy(go.gameObject);
        }

        var leaderboarDictionary = (Dictionary<string, object>)snapshot.Value;
        var _leaderborard = leaderboarDictionary.Values.OrderByDescending(x => int.Parse("" + ((Dictionary<string, object>)x)["score"]));
        foreach (var userDoc in _leaderborard)
        {
            var userObject = (Dictionary<string, object>)userDoc;
 
            var go = GameObject.Instantiate(scoreEntryPrefab, transform);
            go.transform.position = new Vector2(go.transform.position.x, VerticalOffset - (VerticalSpace * i));
            go.GetComponent<ScoreEntry>().SetLabels("" + userObject["username"], "" + userObject["score"]);
            i++;
        }

    }

   public void GetUsersHighestScores()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("users").OrderByChild("score").LimitToLast(3)
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log(task.Exception);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    int i = 1;

                    foreach (var userDoc in (Dictionary<string, object>)snapshot.Value)
                    {
                        var userObject = (Dictionary<string, object>)userDoc.Value;

                        Debug.Log(userObject["username"] + ":" + userObject["score"]);
                        var go = GameObject.Instantiate(scoreEntryPrefab, transform);
                        go.transform.position = new Vector2(go.transform.position.x,130*i);

                        go.GetComponent<ScoreEntry>().SetLabels(""+ userObject["username"], ""+userObject["score"]);

                        i++;
                    }
                }
            });
    }
}
