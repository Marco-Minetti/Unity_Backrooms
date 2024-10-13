using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
    public GameObject objectToSpawn;       // The prefab to spawn
    public float spawnChance = 0.5f;       // Chance to spawn (0 to 1)
    public Vector3[] spawnLocations;       // Array of possible spawn locations

    public AudioClip soundToPlay;           // The sound to play when the object is destroyed
    private AudioSource audioSource;        // Reference to the AudioSource component

    private GameObject spawnedObject;       // Reference to the spawned object

    void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.LogWarning("No AudioSource found on SpawnableObject, one was added.");
        }

        // Try to spawn an object immediately when the spawner starts
        TrySpawn();
    }

    void Update()
    {
        // Continuously check if we can spawn a new object
        if (spawnedObject == null)
        {
            TrySpawn();
        }
    }

    void TrySpawn()
    {
        // Only attempt to spawn if no object is currently spawned
        if (Random.value <= spawnChance)
        {
            // Randomly select a spawn location from the array
            Vector3 spawnPosition = GetRandomSpawnLocation();

            // Instantiate the object at the selected position with the original rotation
            spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

            // Set the spawned object to be interactable
            Interactable interactable = spawnedObject.AddComponent<Interactable>();
            interactable.objectToDestroy = spawnedObject; // Set the reference to this instance

            // Subscribe to the interaction event
            interactable.OnInteract += () => 
            {
                PlaySound(); // Play sound when the object is destroyed
                DestroySpawnedObject();
            };

            Debug.Log(objectToSpawn.name + " has been spawned at " + spawnPosition);
        }
        else
        {
            Debug.Log("Object did not spawn this time.");
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
            Debug.LogError("AudioClip is not assigned in the Inspector! Please assign it.");
        }
    }

    void DestroySpawnedObject()
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject); // Destroy the currently spawned object
            spawnedObject = null; // Clear the reference to allow spawning a new object
        }
    }

    Vector3 GetRandomSpawnLocation()
    {
        if (spawnLocations.Length > 0)
        {
            // Return a random spawn location from the array
            return spawnLocations[Random.Range(0, spawnLocations.Length)];
        }
        else
        {
            Debug.LogWarning("No spawn locations defined!");
            return transform.position; // Fallback to the spawner's position if no locations are set
        }
    }
}
