using UnityEngine;

public class PlaySoundOnKeyPress : MonoBehaviour
{
    public AudioClip soundToPlay;           // The sound to play when "E" is pressed
    public GameObject objectToDestroy;      // Reference to the object to destroy on interaction (optional)
    public delegate void InteractAction();  // Delegate for interaction events
    public event InteractAction OnInteract; // Event to call when interacted
    private AudioSource audioSource;        // Reference to the AudioSource component
    private bool playerNearby = false;      // Flag to track if the player is nearby

    void Start()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();

        // Check if AudioSource is not found
        if (audioSource == null)
        {
            // If there's no AudioSource component, add one
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Check if the player is nearby and presses the "E" key
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PlaySound();

            // Invoke the interaction event
            OnInteract?.Invoke();

            // Optionally destroy an object upon interaction
            if (objectToDestroy != null)
            {
                Destroy(objectToDestroy);
            }
        }
    }

    void PlaySound()
    {
        if (soundToPlay != null)
        {
            audioSource.PlayOneShot(soundToPlay);
        }
        else
        {
            Debug.LogError("Sound to play is not assigned! Please assign it in the inspector.");
        }
    }

    // Detect when the player enters the interaction zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure it's the player
        {
            playerNearby = true;
        }
    }

    // Detect when the player exits the interaction zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
