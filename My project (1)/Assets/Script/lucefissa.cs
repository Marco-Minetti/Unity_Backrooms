using UnityEngine;

public class ObjectFlickerOnTrigger : MonoBehaviour
{
    public GameObject objectToControl;         // The object to appear/disappear
    public float flickerDuration = 2f;         // Duration of flickering before the object stays visible
    public float initialFlickerFrequency = 0.2f;// Starting frequency of flickering
    public float frequencyDecreaseFactor = 1.2f; // Factor by which the frequency increases (slows flicker)

    private bool playerInside = false;         // Is the player in the trigger zone?
    private bool isFlickering = false;         // Is the object currently flickering?
    private bool hasFlickered = false;         // Prevents multiple retriggers
    private float flickerTimer = 0f;           // Timer to track the flicker duration
    private float flickerIntervalTimer = 0f;   // Timer for the on/off flicker frequency
    private float currentFlickerFrequency;     // The current flicker frequency
    private bool objectVisible = false;        // Is the object currently visible?

    void Start()
    {
        objectToControl.SetActive(false);      // Start with the object inactive (invisible)
        currentFlickerFrequency = initialFlickerFrequency; // Set the initial flicker frequency
    }

    void Update()
    {
        if (playerInside && isFlickering)
        {
            FlickerObjectOnOff();
        }
    }

    private void FlickerObjectOnOff()
    {
        flickerTimer += Time.deltaTime;
        flickerIntervalTimer += Time.deltaTime;

        // Toggle the object on/off at the current flicker frequency
        if (flickerIntervalTimer >= currentFlickerFrequency)
        {
            objectVisible = !objectVisible;    // Toggle the object state
            objectToControl.SetActive(objectVisible); // Show/hide the object
            flickerIntervalTimer = 0f;         // Reset the flicker interval timer

            // Decrease flicker frequency (increase the interval) after each toggle
            currentFlickerFrequency *= frequencyDecreaseFactor;
        }

        // Stop flickering after flickerDuration and keep the object fully visible
        if (flickerTimer >= flickerDuration)
        {
            isFlickering = false;
            hasFlickered = true;               // Mark as fully flickered
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
            objectVisible = false;      // Start with the object hidden
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

            if (!isFlickering) // If flickering is done, hide the object
            {
                objectToControl.SetActive(false);  // Hide the object when the player leaves
                hasFlickered = false;              // Allow flickering again on next entry
            }
        }
    }
}
