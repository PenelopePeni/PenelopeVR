using UnityEngine;

public class VRPainter : MonoBehaviour
{
    public Texture2D brushTexture; // Textura del pincel
    public GameObject Soldador; // Objeto con el que se dibuja
    private RenderTexture tempRenderTexture; // RenderTexture temporal para pintar encima

    private void Start()
    {
        // Crear RenderTexture temporal para mezclar dibujos sin sobrescribir
        tempRenderTexture = new RenderTexture(1024, 1024, 0);
    }

    private void Update()
    {
        if (Soldador != null)
        {
            Paint();
        }
    }

    void Paint()
    {
        RaycastHit hit;
        if (Physics.Raycast(Soldador.transform.position, Soldador.transform.forward, out hit))
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null)
            {
                // Verificar si el objeto ya tiene una RenderTexture
                if (!(rend.material.mainTexture is RenderTexture targetTexture))
                {
                    // Si no tiene RenderTexture, crear una y asignarla
                    targetTexture = new RenderTexture(1024, 1024, 0);
                    rend.material.mainTexture = targetTexture;
                }

                // Guardar la textura actual en una temporal antes de pintar
                Graphics.Blit(targetTexture, tempRenderTexture);

                // Pintar encima sin borrar lo anterior
                Graphics.Blit(brushTexture, targetTexture);

                // Restaurar la textura anterior con los nuevos cambios
                Graphics.Blit(tempRenderTexture, targetTexture);
            }
        }
    }
}
