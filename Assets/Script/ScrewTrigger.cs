using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ScrewTrigger : MonoBehaviour
{
    [SerializeField] private Animator screwAnimator;
    [SerializeField] private GameObject requiredObject; // Objeto que debe entrar en el trigger
    [SerializeField] private InputActionProperty triggerInput; // Acción del botón trigger
    [SerializeField] private GameObject targetPositionObject; // Posición final del tornillo
    [SerializeField] private float animationDuration = 1f; // Tiempo de espera serializado
    [SerializeField] private GameObject physicsObject; // Objeto al que se le volverán a aplicar las físicas
    [SerializeField] private Rigidbody kinematicTarget; // Objeto cuyo isKinematic se modificará
    [SerializeField] private GameObject objectToMove; // Objeto que cambiará de posición
    
    private Rigidbody screwRigidbody;
    private bool isObjectInside = false;
    private bool isAnimating = false;

    void Start()
    {
        screwRigidbody = GetComponent<Rigidbody>();
        if (screwRigidbody != null)
        {
            screwRigidbody.isKinematic = true; // Desactiva las físicas al inicio
        }
        
        if (kinematicTarget != null)
        {
            kinematicTarget.isKinematic = true; // Se asegura de que el objeto designado sea kinematic al inicio
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == requiredObject)
        {
            isObjectInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == requiredObject)
        {
            isObjectInside = false;
            screwAnimator.SetBool("IsActive", false); // Detiene la animación si el objeto sale
        }
    }

    void Update()
    {
        bool triggerPressed = triggerInput.action.ReadValue<float>() > 0.1f;
        
        if (isObjectInside && triggerPressed && !isAnimating)
        {
            screwAnimator.SetBool("IsActive", true);
            StartCoroutine(WaitForAnimationEnd());
        }
    }

    private IEnumerator WaitForAnimationEnd()
    {
        isAnimating = true;
        yield return new WaitForSeconds(animationDuration); // Espera el tiempo serializado
        OnAnimationEnd();
        isAnimating = false;
    }

    private void OnAnimationEnd()
    {
        if (targetPositionObject != null && objectToMove != null)
        {
            objectToMove.transform.position = targetPositionObject.transform.position; // Mueve el objeto a la posición final
            if (physicsObject != null)
            {
                Rigidbody objRigidbody = physicsObject.GetComponent<Rigidbody>();
                if (objRigidbody != null)
                {
                    objRigidbody.isKinematic = false; // Reactiva las físicas en el objeto especificado
                }
            }
            
            if (kinematicTarget != null)
            {
                kinematicTarget.isKinematic = false; // Desactiva isKinematic en el objeto especificado
            }
        }
    }
}
