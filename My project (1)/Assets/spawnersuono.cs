using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
    public GameObject objectToSpawn;       // The prefab to spawn
    public float spawnChance = 0.5f;       // Chance to spawn (0 to 1)
    public List<Vector3> spawnLocations;       // Array of possible spawn locations
    public AudioSource timer;
    public AudioSource soundToPlay;           // The sound to play when the object is destroyed
    public AudioSource urloMasculino;        // Reference to the AudioSource component

    

    private GameObject spawnedObject;       // Reference to the spawned object

    private int numObject = 0; //to add more object

    private int objects = 0; //how much object


    void Start()
    {
        // Try to spawn an object immediately when the spawner starts
        TrySpawn();
    }

    void Update()
    {
        if(timer.time >= timer.clip.length) {
            for(int i = 0; i < numObject; i++) {
                DestroySpawnedObject();
            }
            urloMasculino.Play();
            
        }
        // Continuously check if we can spawn a new object
        if (objects == 0)
        {
            numObject++;
            PlaySound();
            for(int i = 0; i < numObject; i++) {
                TrySpawn();
            }
        }
        Debug.Log(timer.time);    
        Debug.Log(timer.clip.length);  

    }

    void TrySpawn()
    {
        // Only attempt to spawn if no object is currently spawned

            // Randomly select a spawn location from the array
            Vector3 spawnPosition = GetRandomSpawnLocation();

            // Instantiate the object at the selected position with the original rotation
            spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            objects++;
            spawnedObject.name = "Object_" + objects;
            // Set the spawned object to be interactable
            Interactable interactable = spawnedObject.AddComponent<Interactable>();
            interactable.objectToDestroy = spawnedObject; // Set the reference to this instance

            // Subscribe to the interaction event
            interactable.OnInteract += () => 
            {
                DestroySpawnedObject();
            };

            Debug.Log(objectToSpawn.name + " has been spawned at " + spawnPosition);
       
    }

    void PlaySound()
    {
        soundToPlay.Play();
        timer.Play();
    }

    void DestroySpawnedObject()
    {
        
        objects--;
        
    }

    Vector3 GetRandomSpawnLocation()
    {
        
        int randoms = Random.Range(2, spawnLocations.Count);
            // Return a random spawn location from the array
        Vector3 posizione = spawnLocations[randoms];
            
        spawnLocations.RemoveAt(randoms);

        return posizione;
    }
}
