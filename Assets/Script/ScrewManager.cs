using UnityEngine;

public class ScrewManager : MonoBehaviour
{
    [SerializeField] private PuertaControl puerta;
    [SerializeField] private int totalTornillos = 4; // Número total de tornillos

    private int tornillosQuitados = 0;

    public void ScrewRemoved()
    {
        tornillosQuitados++;
        Debug.Log("Tornillos completados: " + tornillosQuitados);

        if (tornillosQuitados >= totalTornillos)
        {
            puerta.AbrirPuerta(); // Abre la puerta cuando los tornillos se han retirado
        }
    }
}