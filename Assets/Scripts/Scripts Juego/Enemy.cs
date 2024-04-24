using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private float velocidad; // Velocidad a la que se mueve el enemigo
    [SerializeField] private float tiempoInmovil = 1f; // Tiempo que el enemigo permanece inmóvil después de recibir daño
    [SerializeField] private GameObject balaPrefab; // Prefab de la bala que disparará el enemigo
    [SerializeField] private Transform puntoDisparo; // Punto desde el cual se instanciará la bala
    [SerializeField] private float tiempoEntreDisparos = 3f; // Tiempo entre cada disparo
    private bool puedeDisparar = true;
    private Transform jugador; // Referencia al transform del jugador
    private bool inmovilizado = false; // Indica si el enemigo está inmovilizado

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Jugador").transform; // Buscar al jugador por el tag y obtener su transform
    }

    private void Update()
    {
        // Si el enemigo no está inmovilizado y hay un jugador y el enemigo está vivo, moverse hacia el jugador
        if (!inmovilizado && jugador != null && vida > 0)
        {
            Vector3 direccion = jugador.position - transform.position; // Obtener la dirección hacia el jugador
            direccion.Normalize(); // Normalizar la dirección para mantener la misma velocidad en todas las direcciones
            transform.position += direccion * velocidad * Time.deltaTime; // Moverse hacia el jugador a una velocidad constante
        }
        if (puedeDisparar && vida > 0)
        {
            StartCoroutine(Disparar());
        }
    }
    private IEnumerator Disparar()
    {
        puedeDisparar = false; // Desactiva la posibilidad de disparar temporalmente
        Instantiate(balaPrefab, puntoDisparo.position, Quaternion.identity);
        yield return new WaitForSeconds(tiempoEntreDisparos);
        puedeDisparar = true; // Reactiva la posibilidad de disparar
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            Muerte();
        }
        else
        {
            StartCoroutine(InmovilizarPorSegundos(tiempoInmovil));
        }
    }

    private IEnumerator InmovilizarPorSegundos(float segundos)
    {
        inmovilizado = true; // Marcar al enemigo como inmovilizado
        yield return new WaitForSeconds(segundos); // Esperar el tiempo especificado
        inmovilizado = false; // Permitir que el enemigo se mueva nuevamente
    }

    private void Muerte()
    {
        Destroy(gameObject);
        FindObjectOfType<ProtaController>().IncrementarPuntuacion(1);
    }
}
