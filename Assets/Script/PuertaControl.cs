using UnityEngine;
using System.Collections;

public class PuertaControl : MonoBehaviour
{
    [SerializeField] private Transform puerta; // Objeto que actuará como bisagra
    [SerializeField] private GameObject[] tornillos = new GameObject[4]; // Array con los tornillos

    private int tornillosFuera = 0; // Contador de tornillos que han salido
    private bool yaGirado = false; // Evita múltiples rotaciones
    private Quaternion rotacionInicial;

    void Start()
    {
        if (puerta == null)
        {
            Debug.LogError("Falta asignar la referencia de la bisagra (puerta).");
            return;
        }
        rotacionInicial = puerta.localRotation;
    }

    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto que salió de contacto es uno de los tornillos asignados
        for (int i = 0; i < tornillos.Length; i++)
        {
            if (other.gameObject == tornillos[i])
            {
                tornillosFuera++;

                if (tornillosFuera >= tornillos.Length && !yaGirado)
                {
                    yaGirado = true;
                    StartCoroutine(RotarPuerta());
                }
                break; // Salir del bucle para evitar contar múltiples veces
            }
        }
    }

    IEnumerator RotarPuerta()
    {
        float tiempo = 0f;
        float duracion = 1f; // Duración de la animación en segundos
        Quaternion rotacionFinal = Quaternion.Euler(0, 0, -17.7f) * rotacionInicial; // Rotar en Z

        while (tiempo < duracion)
        {
            puerta.localRotation = Quaternion.Slerp(rotacionInicial, rotacionFinal, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }

        puerta.localRotation = rotacionFinal; // Asegurar la rotación final exacta
    }
}
