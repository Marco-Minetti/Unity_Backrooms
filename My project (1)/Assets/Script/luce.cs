using UnityEngine;

public class LightAndObjectFlickerOnTrigger : MonoBehaviour
{
    public Light spotLight;                    // The spotlight to control
    public GameObject objectToControl;         // The object to appear/disappear
    public float flickerDuration = 2f;         // Duration of flickering before both the light and object stay on
    public float initialFlickerFrequency = 0.2f; // Starting frequency of flickering
    public float frequencyDecreaseFactor = 1.2f; // Factor by which the frequency increases (slows flicker)
    public float targetIntensity = 1.5f;       // Intensity when the light is fully on

    private bool playerInside = false;         // Is the player in the trigger zone?
    private bool isFlickering = false;         // Is flickering happening right now?
    private bool hasFlickered = false;         // Prevents multiple retriggers
    private float flickerTimer = 0f;           // Timer to track the flicker duration
    private float flickerIntervalTimer = 0f;   // Timer for the on/off flicker frequency
    private float currentFlickerFrequency;     // The current flicker frequency
    private bool lightOn = false;              // Is the light currently on or off?
    private bool objectVisible = false;        // Is the object currently visible?

    void Start()
    {
        spotLight.intensity = 0f;              // Start with the light off
        objectToControl.SetActive(false);      // Start with the object hidden
        currentFlickerFrequency = initialFlickerFrequency; // Set the initial flicker frequency
    }

    void Update()
    {
        if (playerInside && isFlickering)
        {
            FlickerLightAndObject();
        }
    }

    private void FlickerLightAndObject()
    {
        flickerTimer += Time.deltaTime;
        flickerIntervalTimer += Time.deltaTime;

        // Toggle the light and object on/off at the current flicker frequency
        if (flickerIntervalTimer >= currentFlickerFrequency)
        {
            // Toggle light
            lightOn = !lightOn;
            spotLight.intensity = lightOn ? targetIntensity : 0f; // Turn light on/off

            // Toggle object visibility
            objectVisible = !objectVisible;
            objectToControl.SetActive(objectVisible); // Show/hide the object

            flickerIntervalTimer = 0f;         // Reset the flicker interval timer

            // Decrease flicker frequency (increase the interval) after each toggle
            currentFlickerFrequency *= frequencyDecreaseFactor;
        }

        // Stop flickering after flickerDuration and keep both the light and object fully on
        if (flickerTimer >= flickerDuration)
        {
            isFlickering = false;
            hasFlickered = true;                // Mark as fully flickered
            spotLight.intensity = targetIntensity; // Keep the light fully on
            objectToControl.SetActive(true);   // Keep the object fully visible
        }
    }

    // Trigger event when the player enters the collision box
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasFlickered) // Ensure it's the player and flicker hasn't occurred
        {
            playerInside = true;
            isFlickering = true;
            flickerTimer = 0f;          // Reset the flicker timer
            flickerIntervalTimer = 0f;  // Reset the flicker interval timer
            lightOn = false;            // Start with the light off
            objectVisible = false;      // Start with the object hidden
            spotLight.intensity = 0f;   // Turn the light off
            objectToControl.SetActive(false);  // Hide the object initially
            currentFlickerFrequency = initialFlickerFrequency; // Reset frequency to initial value
        }
    }

    // Trigger event when the player exits the collision box
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;

            if (!isFlickering) // If flickering is done, turn off the light and hide the object
            {
                spotLight.intensity = 0f;      // Turn the light off
                objectToControl.SetActive(false);  // Hide the object
                hasFlickered = false;          // Allow flickering again on next entry
            }
        }
    }
}
