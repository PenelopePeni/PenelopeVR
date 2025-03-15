using UnityEngine;
using System.Collections;

public class MeshMaterialFadeOut : MonoBehaviour
{
    public Renderer objectRenderer;  // Renderer del objeto 3D
    public Material materialA;  // Primer material
    public Material materialB;  // Segundo material
    public float blinkSpeed = 0.5f;  // Velocidad del parpadeo
    public float jumpForce = 5f;  // Fuerza del salto
    public float fadeSpeed = 1f;  // Velocidad del fade out

    private bool isBlinking = true;
    private bool isFadingOut = false;
    private Rigidbody objectRigidbody;
    private Color startColor;

    void Start()
    {
        // Obtiene el Rigidbody para el salto
        objectRigidbody = GetComponent<Rigidbody>(); // Asegúrate de que el objeto tenga un Rigidbody
        startColor = materialA.color; // Guarda el color inicial del material
        StartCoroutine(BlinkEffect());
    }

    private IEnumerator BlinkEffect()
    {
        while (isBlinking)
        {
            objectRenderer.material = materialA; // Cambia al primer material
            yield return new WaitForSeconds(blinkSpeed); // Espera
            objectRenderer.material = materialB; // Cambia al segundo material
            yield return new WaitForSeconds(blinkSpeed); // Espera
        }
    }

    public void StopBlinking()
    {
        isBlinking = false;
        objectRenderer.material = materialB; // Restaura el material original
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Punta")) // Si el jugador toca el objeto
        {
            StopBlinking(); // Detiene el parpadeo
            Jump(); // Hace que el objeto salte
            StartCoroutine(FadeOut()); // Comienza el fade out
        }
    }

    void Jump()
    {
        // Aplica la fuerza hacia arriba al Rigidbody para que salte
        if (objectRigidbody != null)
        {
            objectRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    IEnumerator FadeOut()
    {
        // Cambia el material a uno con transparencia
        Material materialWithTransparency = new Material(objectRenderer.material);
        materialWithTransparency.SetFloat("_Mode", 3); // Asegura que sea un material transparente
        materialWithTransparency.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        materialWithTransparency.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        materialWithTransparency.SetInt("_ZWrite", 0);
        materialWithTransparency.DisableKeyword("_ALPHATEST_ON");
        materialWithTransparency.EnableKeyword("_ALPHABLEND_ON");
        materialWithTransparency.SetFloat("_Blend", 0.5f);
        
        objectRenderer.material = materialWithTransparency; // Aplica el material con transparencia

        Color color = objectRenderer.material.color;
        while (color.a > 0)
        {
            color.a -= fadeSpeed * Time.deltaTime; // Reduce la opacidad
            objectRenderer.material.color = color;
            yield return null; // Espera un cuadro
        }
    }
}
