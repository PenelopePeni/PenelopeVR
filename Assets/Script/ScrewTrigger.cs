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

    public void OnAnimationEnd()
    {
        if (targetPositionObject != null)
        {
            transform.position = targetPositionObject.transform.position; // Mueve el tornillo a la posición final
            if (screwRigidbody != null)
            {
                screwRigidbody.isKinematic = false; // Reactiva las físicas
            }
        }
    }
}
