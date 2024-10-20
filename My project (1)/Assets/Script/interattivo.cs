using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject objectToDestroy;      // Reference to the object to destroy
    public delegate void InteractAction();  // Delegate for interaction events
    public event InteractAction OnInteract; // Event to call when interacted
    private bool playerNearby = false;      // Flag to track if the player is nearby

    void Update()
    {
        // Check for interaction and if the player is nearby
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player pressed E while nearby."); // Debug log
            // Invoke the interaction event
            OnInteract?.Invoke();

            // Destroy the object if assigned
            if (objectToDestroy != null)
            {
                Destroy(objectToDestroy);
            }
        }
    }

    // Detect when the player enters the interaction zone
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure it's the player
        {
            Debug.Log("Player entered interaction zone."); // Debug log
            playerNearby = true;
        }
    }

    // Detect when the player exits the interaction zone
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited interaction zone."); // Debug log
            playerNearby = false;
        }
    }
}
