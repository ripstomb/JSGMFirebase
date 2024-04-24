using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool gameOver = false;
    public GameObject gameOnScreen;
    public GameObject gameOverScreen;
    public ProtaController Prota;
    private void Start()
    {
        gameOver = false;
        gameOnScreen.SetActive(true);
        gameOverScreen.SetActive(false); 
    }

    private void Update()
    {
        if (Prota.vidaActual <= 0 && !gameOver)
        {
            gameOver = true;
            GameOver();
        }
    }

    private void GameOver()
    {
        GuardarPuntuacion();
        gameOnScreen.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    private void GuardarPuntuacion()
    {
        GameOverController.instance.SaveScore();
    }
}

