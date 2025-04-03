using UnityEngine;
using UnityEngine.InputSystem;

public class DrillController : MonoBehaviour
{
    [SerializeField] public Animator drillAnimator;
    [SerializeField] public AudioSource drillSound;
    [SerializeField] public InputActionProperty triggerInput; // Acción de presionar el gatillo

    private bool isDrillActive = false;

    void Update()
    {
        bool triggerPressed = triggerInput.action.ReadValue<float>() > 0.1f;

        if (triggerPressed && !isDrillActive)
        {
            isDrillActive = true;
            drillAnimator.SetBool("IsActive", true);
            if (drillSound != null) drillSound.Play();
        }
        else if (!triggerPressed && isDrillActive)
        {
            isDrillActive = false;
            drillAnimator.SetBool("IsActive", false);
            if (drillSound != null) drillSound.Stop();
        }
    }
	public void PlayDrillSound()
	{
    	if (drillSound != null) drillSound.Play();
	}
}
