using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreLabel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _label;
    // Start is called before the first frame update

    void Reset()
    {
        _label = GetComponent<TMP_Text>();
    }
    void Start()
    {
        FirebaseDatabase.DefaultInstance
       .GetReference($"users/{ FirebaseAuth.DefaultInstance.CurrentUser.UserId}/score")
       .ValueChanged += HandleValueChanged;
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
      /*  if (args.Snapshot.Value == null)
        {
            _label.text = "SCORE: 0";
            return;
        }*/
        string _score = "" + args.Snapshot.Value;
        int score = string.IsNullOrEmpty(_score)?0:int.Parse(_score);

        _label.text ="SCORE: "+ score;
    }
}
