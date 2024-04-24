using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool gameOver = false; // Variable para indicar si el juego ha terminado
    public GameObject gameOnScreen;
    public GameObject gameOverScreen; // Referencia a la pantalla de Game Over
    public ProtaController Prota;
    private void Start()
    {
        gameOver = false;
        gameOnScreen.SetActive(true);
        gameOverScreen.SetActive(false); // Oculta la pantalla de Game Over al iniciar el juego
    }

    private void Update()
    {
        // Verifica si la vida del jugador es 0 o menos
        if (Prota.vidaActual <= 0 && !gameOver)
        {
            gameOver = true; // Establece el estado de Game Over
            GameOver(); // Llama a la función de Game Over
        }
    }

    private void GameOver()
    {
        // Guarda la puntuación
        GuardarPuntuacion();
        gameOnScreen.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    private void GuardarPuntuacion()
    {
        // Implementa la lógica para guardar la puntuación aquí
        GameOverController.instance.SaveScore();
    }
}

