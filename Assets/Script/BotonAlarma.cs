using System.Collections; // Importa IEnumerator
using UnityEngine; // Necesario para MonoBehaviour
public class BotonAlarma : MonoBehaviour
{
    public AudioSource sonidoMaquina;
  
    private bool alarmaActiva = true;

    void Start()
    {
      
    }


    public void DesactivarAlarma()
    {
        if (!alarmaActiva) return;
        sonidoMaquina.Stop(); // Apagar el sonido de la máquina
        GetComponent<AudioSource>().Stop(); // Apagar sonido de la alarm
        alarmaActiva = false;
    }
}