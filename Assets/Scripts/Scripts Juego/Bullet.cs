using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float velocidad = 10f;
    [SerializeField] private int cantidad = 1; 
    [SerializeField] private float tiempoDeVida = 3f;

    private Transform jugador;
    private Vector2 direccion; 

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Jugador").transform; 
        if (jugador != null)
        {
            direccion = (jugador.position - transform.position).normalized;

            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;
            Destroy(gameObject, tiempoDeVida);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            other.GetComponent<ProtaController>().TomarDaño(cantidad);
            Destroy(gameObject);
        }
    }
}
