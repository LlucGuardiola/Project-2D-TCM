using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DañoRecibido : MonoBehaviour
{
    [SerializeField] int vidas;
    [SerializeField] Slider sliderVidas;
    [SerializeField] Transform puntoRespawn; // Punto de respawn inicial
    [SerializeField] string checkpointTag = "Checkpoint"; // Tag del checkpoint

    private Vector3 posicionInicial; // Posición inicial del jugador
    private Transform ultimoCheckpoint; // Último checkpoint activo

    void Start()
    {
        sliderVidas.maxValue = vidas;
        sliderVidas.value = vidas; // Aseguramos que el valor del slider sea igual al de las vidas al inicio
        
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Deathzone"))
        {
            Debug.Log("Colisión con zona de muerte");
            PerderVida();
        }
        
    }

    void PerderVida()
    {
        vidas--;
        sliderVidas.value = vidas; // Actualizamos el valor del slider

        if (vidas <= 0)
        {
            Debug.Log("Sin vidas, respawn");
            Respawn();
        }
    }

    void Respawn()
    {
        Debug.Log("Respawning at checkpoint: " + ultimoCheckpoint.position);

        // Mover al jugador al último checkpoint activo
        transform.position = ultimoCheckpoint.position;

        // Reiniciar las vidas
        vidas = (int)sliderVidas.maxValue; // Convertimos el valor máximo del slider a int
        sliderVidas.value = vidas; // Actualizar el slider de vidas
    }
}
