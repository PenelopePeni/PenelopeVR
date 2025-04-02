using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExtintorController : MonoBehaviour
{
    public Slider barraProgreso;
    public GameObject gO_Particulas;
    public AudioSource alarmaSonido;
    private bool dentroZona = false;
    private bool extintorUsado = false;
    private float progreso = 0f;

    void Start()
    {
        barraProgreso.gameObject.SetActive(false);
    }

    void Update()
    {
        if (dentroZona && !extintorUsado && Input.GetButton("Fire1")) // Fire1 es el botón de agarre en VR
        {
            progreso += Time.deltaTime / 3f; // Progresión en 3 segundos
            barraProgreso.value = progreso;
            if (progreso >= 1f)
            {
                ApagarFuego();
            }
        }
        else if (!Input.GetButton("Fire1"))
        {
            progreso = 0f;
            barraProgreso.value = 0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZonaFuego")) // La zona donde debe estar el extintor
        {
            dentroZona = true;
            barraProgreso.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ZonaFuego"))
        {
            dentroZona = false;
            barraProgreso.gameObject.SetActive(false);
        }
    }

    void ApagarFuego()
    {
        gO_Particulas.SetActive(false);
        alarmaSonido.Stop();
        barraProgreso.gameObject.SetActive(false);
        extintorUsado = true;
    }
}