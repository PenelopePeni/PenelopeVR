using UnityEngine;
using UnityEngine.InputSystem;  // Para usar el sistema de entradas

public class TaladroController : MonoBehaviour
{
    public Animator taladroAnimator;   // El Animator del taladro
    public AudioSource taladroAudio;   // El AudioSource del taladro
    public InputActionReference triggerAction;  // Acción del gatillo delantero

    private bool isTriggerPressed = false;  // Estado del gatillo

    void Start()
    {
        // Habilitar la acción del gatillo
        triggerAction.action.Enable();
    }

    void Update()
    {
        // Leer el valor del gatillo (Trigger)
        isTriggerPressed = triggerAction.action.ReadValue<float>() > 0.1f;

        // Si el gatillo está presionado, activar la animación y el sonido
        if (isTriggerPressed)
        {
            ActivarAnimacion(true);
            ActivarSonido(true);
        }
        else
        {
            ActivarAnimacion(false);
            ActivarSonido(false);
        }
    }

    // Método para activar/desactivar la animación
    void ActivarAnimacion(bool activar)
    {
        if (taladroAnimator != null)
        {
            taladroAnimator.SetBool("IsGirando", activar);  // Cambiar el parámetro "isGirando" en el Animator
        }
    }

    // Método para activar/desactivar el sonido
    void ActivarSonido(bool activar)
    {
        if (taladroAudio != null)
        {
            if (activar && !taladroAudio.isPlaying)
            {
                taladroAudio.Play();  // Reproducir el sonido
            }
            else if (!activar && taladroAudio.isPlaying)
            {
                taladroAudio.Stop();  // Detener el sonido
            }
        }
    }

    private void OnDestroy()
    {
        // Deshabilitar la acción al destruir el objeto
        triggerAction.action.Disable();
    }
}
