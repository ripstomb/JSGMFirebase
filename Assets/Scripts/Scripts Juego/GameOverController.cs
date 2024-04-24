using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public static GameOverController instance;
    public TMP_Text scoreText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
       // HandleGameOver();
    }
    private void Update()
    {
        scoreText.text ="Kills: "+ ProtaController.puntuacion.ToString();

    }
    public async void SaveScore()
    {
        print("<b>Score Saved!</b>");

        string uid = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(uid).Child("lastScore").SetValueAsync(ProtaController.puntuacion);

        var result = await FirebaseDatabase.DefaultInstance.GetReference($"users/{uid}/score")
            .GetValueAsync();
        string _score = "" + result.Value;
        int lastBestScore = string.IsNullOrEmpty(_score) ? 0 : int.Parse(_score);

        if(ProtaController.puntuacion > lastBestScore)
            FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(uid).Child("score").SetValueAsync(ProtaController.puntuacion);
    }
}
