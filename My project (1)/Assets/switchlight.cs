using UnityEngine;

public class SwitchLightControl : MonoBehaviour
{
    private bool playerNearby = false;    // To track if the player is near the switch
    private bool lightsAreOn = true;      // To track the current state of the lights (default is on)

    public Light[] lightsToControl;       // Array of lights to be turned off/on
    public int oppositeLightIndex = 0;    // Index of the light that should behave in the opposite way
    public AudioClip toggleSound;         // Sound to play when toggling the lights
    private AudioSource audioSource;      // Reference to the AudioSource component

    // Fog control variables
    public float fogDensityOn = 0.05f;    // Fog density when lights are off
    public float fogDensityOff = 0.01f;   // Fog density when lights are on
    private float currentFogDensity;

    void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.LogWarning("No AudioSource found on SwitchLightControl, one was added.");
        }

        // Initialize fog settings
        RenderSettings.fog = true; // Ensure fog is enabled
        currentFogDensity = RenderSettings.fogDensity = fogDensityOff; // Start with low fog density
    }

    void Update()
    {
        // Check if the player is nearby and the "E" key is pressed (only to turn off the lights)
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && lightsAreOn)
        {
            // Turn off the lights and play the toggle sound
            ToggleLights(false); // Turn lights off
            PlayToggleSound();
            ToggleFog();
            Debug.Log("Lights turned off!");
        }
    }

    // Toggle the lights on or off
    void ToggleLights(bool state)
    {
        for (int i = 0; i < lightsToControl.Length; i++)
        {
            if (i == oppositeLightIndex)
            {
                // Toggle the opposite light in the reverse way
                lightsToControl[i].enabled = !state;
            }
            else
            {
                // Toggle the other lights normally
                lightsToControl[i].enabled = state;
            }
        }

        // Update the lights' state
        lightsAreOn = state;
    }

    // Play the toggle sound effect
    void PlayToggleSound()
    {
        if (toggleSound != null)
        {
            audioSource.PlayOneShot(toggleSound);
        }
        else
        {
            Debug.LogWarning("No sound assigned for light toggle.");
        }
    }

    // Toggle the fog density
    void ToggleFog()
    {
        if (lightsAreOn)
        {
            RenderSettings.fogDensity = fogDensityOff; // Low fog when lights are on
        }
        else
        {
            RenderSettings.fogDensity = fogDensityOn; // Increase fog when lights are off
        }

        currentFogDensity = RenderSettings.fogDensity;
    }

    // Detect when the player enters the switch's collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            // Automatically turn on lights when entering the collider, if they're off
            if (!lightsAreOn)
            {
                ToggleLights(true); // Turn lights on
                PlayToggleSound();
                ToggleFog();
                Debug.Log("Lights turned on!");
            }
        }
    }

    // Detect when the player exits the switch's collider
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
