using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void CambiarAEscena()
    {
        SceneManager.LoadScene("SignUpScene");
    }
    public void ALeaderboard()
    {
        SceneManager.LoadScene("LeaderBoard");
    }
    public void AUsuarioRegistrado()
    {
        SceneManager.LoadScene("Game");
    }
    public void AJuego()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void ANewContrasenha()
    {
        SceneManager.LoadScene("RestorePassword");
    }
    public void AHome()
    {
        SceneManager.LoadScene("Home");
    }
}
