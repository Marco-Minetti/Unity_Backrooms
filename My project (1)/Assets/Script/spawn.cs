using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnWhenPlayerIsNearby : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToSpawn; // The prefab to spawn
    [SerializeField]
    private GameObject player;         // Reference to the player
    [SerializeField]
    private float destroyDistance = 10f; // Distance to destroy the object
    [SerializeField]
    private int prob = 50;             // Probability of spawning (0-100)

    [SerializeField]
    [Range(0f, 360f)]
    private float minYRotation = 0f;   // Minimum Y rotation
    [SerializeField]
    [Range(0f, 360f)]
    private float maxYRotation = 360f;
    [SerializeField]  
     private GameObject otherObject;// Maximum Y rotation
    private bool hasSpawned = false;     // Track if the object has already spawned
    private GameObject spawnedObject;     // To keep track of the spawned object

    
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.gameObject == player)
        {
            CheckSpawn();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Continuously check while the player is in the trigger
        if (other.gameObject == player)
        {
            CheckSpawn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the colliding object is the player
        if (other.gameObject == player && spawnedObject != null)
        {
            DestroyObject();
        }
    }

    // Function to check if the object should spawn
    private void CheckSpawn()
    {
        // If the object hasn't been spawned yet
        if (!hasSpawned)
        {
            int randomValue = Random.Range(0, 100);
            
            
            // Check if the spawn chance is met
            if (randomValue < prob)
            {
                SpawnObject();
            }
        }
    }
    // Function to spawn the object
    private void SpawnObject()
    {
        
        

        // Generate a random Y rotation within the specified range
        float randomYRotation = Random.Range(minYRotation, maxYRotation);

        Quaternion rotation = Quaternion.Euler(0,randomYRotation,0);

        spawnedObject = Instantiate(objectToSpawn, transform.position, rotation);

        hasSpawned = true; // Mark that the object has been spawned
    }

    // Function to destroy the spawned object
    private void DestroyObject()
    {
        Destroy(spawnedObject);
        spawnedObject = null; // Reset the reference
        hasSpawned = false; // Mark that the object can spawn again
    }

    // Optional: Draw the spawn radius in the scene view
    private void OnDrawGizmosSelected()
    {
        // Draw the Box Collider area (yellow)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
}
