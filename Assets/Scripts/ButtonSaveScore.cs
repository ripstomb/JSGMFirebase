using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSaveScore : MonoBehaviour
{
    [SerializeField]
    private Button _saveScoreButton;

    [SerializeField]
    private TMP_InputField _scoreInputField;

    private void Reset()
    {
        _saveScoreButton = GetComponent<Button>();
        _scoreInputField = GameObject.Find("InputFieldScore").GetComponent<TMP_InputField>();
    }
    void Start()
    {
        _saveScoreButton.onClick.AddListener(HandleLoginButtonClicked);
    }

    private void HandleLoginButtonClicked()
    {
        int score = int.Parse(_scoreInputField.text);
        string uid = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(uid).Child("score").SetValueAsync(score);
    }

}
