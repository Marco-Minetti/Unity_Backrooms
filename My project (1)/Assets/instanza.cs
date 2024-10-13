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
        Vector3[] newLocations = new Vector3[spawner.spawnLocations.Length + 1];

        // Copy existing locations to the new array
        for (int i = 0; i < spawner.spawnLocations.Length; i++)
        {
            newLocations[i] = spawner.spawnLocations[i];
        }

        // Add this location's position to the end of the new array
        
        newLocations[newLocations.Length - 1] = transform.position;
        

        // Replace the old array with the new one
        spawner.spawnLocations = newLocations;

        Debug.Log("Spawn location added: " + transform.position);
    }
}
