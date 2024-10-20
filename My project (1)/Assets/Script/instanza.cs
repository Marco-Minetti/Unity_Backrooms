using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
    public SpawnableObject spawner; // Reference to the SpawnableObject script

    void Start()
    {
        // Check if the spawner is assigned
        if (spawner != null)
        {
            // Add this spawn location's position to the spawner's spawn locations
            AddSpawnLocation();
        }
        else
        {
            Debug.LogWarning("Spawner not assigned in " + gameObject.name);
        }
    }

     void  AddSpawnLocation()
    {
        // Create a new array that is one element larger than the current spawn locations
        
        // Replace the old array with the new one
        spawner.spawnLocations.Add(transform.position);

        Debug.Log("Spawn location added: " + transform.position);
    }
}
