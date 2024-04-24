using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float velocidad = 10f; // Velocidad de la bala
    [SerializeField] private int cantidad = 1; // Da�o que causa la bala al jugador
    [SerializeField] private float tiempoDeVida = 3f;

    private Transform jugador; // Referencia al transform del jugador
    private Vector2 direccion; // Direcci�n hacia el jugador

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Jugador").transform; // Obtener el transform del jugador
        if (jugador != null)
        {
            // Calcular la direcci�n hacia el jugador
            direccion = (jugador.position - transform.position).normalized;

            // Aplicar fuerza en la direcci�n hacia el jugador
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;
            Destroy(gameObject, tiempoDeVida);
        }
        else
        {
            // Si no se encuentra al jugador, destruye la bala
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            // Reducir la vida del jugador cuando la bala colisiona con �l
            other.GetComponent<ProtaController>().TomarDa�o(cantidad);
            Destroy(gameObject);
        }
    }
}
