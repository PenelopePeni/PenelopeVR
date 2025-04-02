using System.Collections; // Importa IEnumerator
using UnityEngine; // Necesario para MonoBehaviour

public class AlarmaManager : MonoBehaviour
{
    public GameObject gO_Particulas;
    public AudioSource alarmaSonido;
    public AudioSource sonidoMaquina;
    public GameObject botonInteractivo;

    void Start()
    {
        StartCoroutine(IniciarAlarma());
    }

    IEnumerator IniciarAlarma()
    {
        yield return new WaitForSeconds(180); // Espera 3 minutos

        gO_Particulas.SetActive(true);
        if (alarmaSonido != null) alarmaSonido.Play();

        
    }
}
