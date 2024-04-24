using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] 
    private Transform controladorGolpe;
    [SerializeField] 
    private float radioGolpe;
    [SerializeField] 
    private float dañoGolpe1;
    [SerializeField]
    private float dañoGolpe2;
    [SerializeField] 
    private float tiempoEntreAtaques;
    [SerializeField] 
    private float tiempoSiguienteAtaque;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && tiempoSiguienteAtaque <= 0)
        {
            Golpe(0);
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }
        if (Input.GetButtonDown("Fire2") && tiempoSiguienteAtaque <= 0)
        {
            Golpe(1);
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }
    }

    private void Golpe(int tipoGolpe)
    {
        switch (tipoGolpe)
        {
            case 0: // Primer golpe
                animator.SetTrigger("GolpePrincipal"); // Se activa la animación del primer golpe.
                break;
            case 1: // Segundo golpe
                animator.SetTrigger("Golpe2"); // Se activa la animación del segundo golpe.
                break;
            case 2: // Tercer golpe
                animator.SetTrigger("Golpe3"); // Se activa la animación del tercer golpe.
                break;
            default:
                break;
        }

        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                if (Input.GetButtonDown("Fire1"))
                    {
                    colisionador.transform.GetComponent<Enemy>().TomarDaño(dañoGolpe1);
                }
                if (Input.GetButtonDown("Fire2"))
                {
                    colisionador.transform.GetComponent<Enemy>().TomarDaño(dañoGolpe2);
                }

            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
