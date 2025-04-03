using UnityEngine;
using System.Collections;

public class ScrewTrigger : MonoBehaviour
{
    [SerializeField] private Animator screwAnimator;
    [SerializeField] private GameObject targetPositionObject; // Posición final del tornillo
    [SerializeField] private float animationDuration = 1f; // Duración de la animación
    [SerializeField] private GameObject physicsObject; // Objeto al que se le activarán las físicas
    [SerializeField] private ScrewManager screwManager; // Referencia al gestor de tornillos

    private bool isAnimating = false;
    private bool isCompleted = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drill") && !isAnimating) // Asegúrate de que el taladro tiene la tag "Drill"
        {
            screwAnimator.SetBool("IsActive", true);
            StartCoroutine(ProcessScrew());
        }
    }

    private IEnumerator ProcessScrew()
    {
        isAnimating = true;
        yield return new WaitForSeconds(animationDuration); // Espera el tiempo de animación

        // Mueve el tornillo a la posición final
        transform.position = targetPositionObject.transform.position;
        isCompleted = true;

        // Reactiva las físicas
        if (physicsObject != null)
        {
            Rigidbody objRigidbody = physicsObject.GetComponent<Rigidbody>();
            if (objRigidbody != null) objRigidbody.isKinematic = false;
        }

        screwManager.ScrewRemoved(); // Notifica que un tornillo ha sido removido
        isAnimating = false;
    }
}