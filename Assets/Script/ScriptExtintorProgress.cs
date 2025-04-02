using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ExtintorProgress : MonoBehaviour
{
    [Header("Configuración")]
    public Slider progressBar;  // Asigna el Slider desde el Inspector
    public float fillSpeed = 0.5f;  // Velocidad de llenado (segundos)

    [Header("Zona de Activación")]
    public Collider requiredZone;  // Zona donde debe estar el objeto
    public GameObject requiredObject; // Objeto específico que debe entrar en la zona

    private bool isFilling = false;  // Controla si se está llenando
    private bool isInsideZone = false; // Verifica si está dentro del collider
    private Coroutine fillCoroutine; // Guarda la referencia de la Corrutina

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == requiredObject) // Solo si el objeto correcto entra
        {
            isInsideZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == requiredObject)
        {
            isInsideZone = false;
            StopFilling();  // Detiene la barra si el objeto sale
        }
    }

    public void StartFilling()
    {
        if (!isFilling && isInsideZone) // Solo si el objeto correcto está dentro del área
        {
            isFilling = true;
            fillCoroutine = StartCoroutine(FillProgress());
        }
    }

    public void StopFilling()
    {
        isFilling = false;
        if (fillCoroutine != null)
            StopCoroutine(fillCoroutine);
    }

    private IEnumerator FillProgress()
    {
        while (isFilling && progressBar.value < 1)
        {
            progressBar.value += Time.deltaTime / fillSpeed;
            yield return null;  // Espera hasta el siguiente frame
        }

        if (progressBar.value >= 1)
        {
            ExtinguishFire();
        }
    }

    private void ExtinguishFire()
    {
        Debug.Log("🔥 Fuego extinguido");
        GameObject.Find("gO_AlarmaSonido").GetComponent<AudioSource>().Stop();
        GameObject.Find("gO_Particulas").SetActive(false);
    }
}
