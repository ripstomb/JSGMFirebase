using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public float velocidadRotacion = 5.0f;

    void Update()
    {
        // Obtener la posici�n del rat�n en la pantalla
        Vector3 posicionRaton = Input.mousePosition;

        // Convertir la posici�n del rat�n de pantalla a un rayo en el mundo
        Ray rayo = Camera.main.ScreenPointToRay(posicionRaton);
        Plane plano = new Plane(Vector3.up, Vector3.zero);
        float distancia;
        plano.Raycast(rayo, out distancia);

        // Obtener la posici�n en el mundo donde el rayo intersecta con el plano
        Vector3 puntoInterseccion = rayo.GetPoint(distancia);

        // Calcular la direcci�n desde la c�mara hacia el punto de intersecci�n
        Vector3 direccion = puntoInterseccion - transform.position;

        // Rotar gradualmente hacia la direcci�n del rat�n
        Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, velocidadRotacion * Time.deltaTime);
    }
}
