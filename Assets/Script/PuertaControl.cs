using UnityEngine;
using System.Collections;

public class PuertaControl : MonoBehaviour
{
    [SerializeField] private Transform puerta; // Objeto de la puerta
    private Quaternion rotacionInicial;
    private Quaternion rotacionFinal;
    private float duracion = 1f; // Duración de la animación

    void Start()
    {
        if (puerta == null)
        {
            Debug.LogError("Falta asignar la referencia de la bisagra (puerta).");
            return;
        }
        rotacionInicial = puerta.localRotation;
        rotacionFinal = Quaternion.Euler(0, 0, -90f) * rotacionInicial; // Ajusta según sea necesario
    }

    public void AbrirPuerta()
    {
        Debug.Log("¡Todos los tornillos han salido! Abriendo la puerta...");
        StartCoroutine(RotarPuerta());
    }

    private IEnumerator RotarPuerta()
    {
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            puerta.localRotation = Quaternion.Slerp(rotacionInicial, rotacionFinal, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }

        puerta.localRotation = rotacionFinal;
    }
}