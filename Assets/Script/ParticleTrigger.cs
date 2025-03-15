using UnityEngine;
using System.Collections; // Necesario para usar Coroutines

public class ParticleTrigger : MonoBehaviour
{
    public ParticleSystem particleSystem;  // Sistema de partículas a activar
    public GameObject bisagra;  // Objeto que desactivará las partículas

    private bool isBisagraActive = false;

    void Start()
    {
        // Inicia la activación de partículas después de 10 segundos
        StartCoroutine(ActivateParticlesAfterDelay(10f));
    }

    private IEnumerator ActivateParticlesAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera el tiempo indicado
        particleSystem.Play(); // Activa las partículas
    }

    void Update()
    {
        // Si la Bisagra se activa, detener las partículas
        if (bisagra.activeSelf && !isBisagraActive)
        {
            isBisagraActive = true;
            particleSystem.Stop(); // Detiene las partículas
        }
    }
}
