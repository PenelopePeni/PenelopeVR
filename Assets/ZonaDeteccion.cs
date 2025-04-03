using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZonaDeteccion : MonoBehaviour
{
    [SerializeField] private ScrewManager screwManager;  // Referencia al ScrewManager para actualizar el contador
    
    // Se llamará cuando un objeto salga del collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tornillo"))  // Verifica si el objeto que salió es un tornillo
        {
            screwManager.ScrewRemoved();  // Llama al método para contar el tornillo que ha salido
        }
    }
}


